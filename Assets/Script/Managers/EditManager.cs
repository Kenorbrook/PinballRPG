using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

namespace Script.Managers
{
    public class EditManager : MonoBehaviour
    {

        [SerializeField]
        private InputField _inputField;
        
        [SerializeField]
        private GameObject _savingLevel;

        [SerializeField]
        private Transform _stoneParent;

        public static GameObject ChoosenObject;
        
        private void Start()
        {
            ChoosenObject = null;
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                }
                else
                    Debug.Log(response.Error);
            });
        }

        public void Play()
        {
        }


        public void AddObject()
        {
        }
        
        public void Send()
        {
            List<Enemy> _enemies = new List<Enemy>(_savingLevel.GetComponentsInChildren<Enemy>());
            List<Spikes> _spikes = new List<Spikes>(_savingLevel.GetComponentsInChildren<Spikes>());
            //Первый объект это всегда он... Учитываем это...
            List<Transform> _stones = new List<Transform>(_stoneParent.GetComponentsInChildren<Transform>());
            string _content = "";
            foreach (var _obj in _enemies)
            {
                var _transform = _obj.transform;
                _content += "Enemy "+_transform.position+" " +_transform.rotation+" " +_transform.localScale +'\n';
            }

            _content += '\n';
            foreach (var _obj in _spikes)
            { var _transform = _obj.transform;
                _content += "Spike "+_transform.position+" " +_transform.rotation+" "+_transform.localScale +'\n';
            }
            _content += '\n';
            foreach (var _obj in _stones)
            {
                _content += "Stone "+_obj.position+" "+_obj.rotation+" "+_obj.localScale+'\n';
            }
            _content += '\n';

            Debug.Log(_content);
            string _levelName=_inputField.text;
            
            LootLockerSDKManager.CreatingAnAssetCandidate(_levelName, response =>
            {
                if (response.success)
                {
                    UploadLevelData(response.asset_candidate_id);
                }
                else
                {
                    Debug.Log(response.Error);
                }
            } );

            string _path = "Assets/Screenshots/Level-data.txt";
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }

            File.WriteAllText(_path, _content);
        }

        public void UploadLevelData(int levelID)
        {
            string _screenFilePath = "Assets/Screenshots/Level-screen.png";
            LootLocker.LootLockerEnums.FilePurpose _screenShot =
                LootLocker.LootLockerEnums.FilePurpose.primary_thumbnail;
            OpenUpload();
            LootLockerSDKManager.AddingFilesToAssetCandidates(levelID, _screenFilePath,"Level-screen.png", _screenShot,
                (screenshotsResponse) =>
                {
                    if (screenshotsResponse.success)
                    {
                        string _textFilePath = "Assets/Screenshots/Level-data.txt";
                        LootLocker.LootLockerEnums.FilePurpose _textShot =
                            LootLocker.LootLockerEnums.FilePurpose.file;
                        LootLockerSDKManager.AddingFilesToAssetCandidates(levelID, _textFilePath, "Level-data.txt", _textShot,
                            (textresponse) =>
                            {
                                if (textresponse.success)
                                {
                                    LootLockerSDKManager.UpdatingAnAssetCandidate(levelID, true, (response) =>
                                    {
                                        
                                    });
                                }
                                else
                                {
                                    Debug.Log(textresponse.Error);
                                }
                            });
                    }
                    else
                    {
                        Debug.Log(screenshotsResponse.Error);
                    }
                });
        }
       

        public void TakeScreenshot()
        {
            string _path = Directory.GetCurrentDirectory() + "/Assets/Screenshots/";
            ScreenCapture.CaptureScreenshot(Path.Combine(_path, "Level-screen.png"));
        }

       private IEnumerator WaitScreenshot()
        {
            TakeScreenshot();
            yield return new WaitForSeconds(1f);
        }

       public void OpenUpload()
       {
           StartCoroutine(WaitScreenshot());
       }
    }
}