using System;
using UnityEngine;

namespace Script
{
    public class FirstCircleBoss : MonoBehaviour, IBoss
    {
        [SerializeField]
        private GameObject[] Spikes;

        [SerializeField]
        private GameObject _wallSpikes;
        
        private int _stage = 0;

        [SerializeField]
        private MovementPath _secondPath;

        public float GetDamage(int hp, int maxHp, float currentScale)
        {
            if (Math.Abs(hp - maxHp / 4) < 20 && _stage <3)
            {
                _stage++;
                GetComponent<MovingObj>().Path = _secondPath;
                GetComponent<MovingObj>().MyStart();
                GetComponent<MovingObj>().speed = 2;
                return currentScale*3f/4f;
            }

            if (Math.Abs(hp - maxHp / 2) < 20&& _stage < 2)
            {
                _stage++;
                Invoke( nameof(ThirdStage),0.5f);
                return currentScale*3f/4f;
            }

            if (Math.Abs(hp - maxHp * 3f / 4) < 20&& _stage < 1)
            {
                _stage++;
               Invoke( nameof(SecondStage),0.5f);
                return currentScale*3f/4f;
            }

            return currentScale;
        }


        public void StartAnim()
        {
            _wallSpikes.SetActive(true);
        }
        
        public void EndAnim()
        {
            _wallSpikes.SetActive(false);
        }
        
        private void SecondStage()
        {
            Debug.Log("Second Stage boss");
            Spikes[Spikes.Length - 1].SetActive(true);
        }
        private void ThirdStage()
        {
            
            Debug.Log("Third Stage boss");
            Spikes[1].SetActive(true);
            Spikes[2].SetActive(true);
            Spikes[Spikes.Length - 1].SetActive(false);
        }
    }
}
