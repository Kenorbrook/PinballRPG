using Michsky.MUIP;
using UnityEngine;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private SliderManager _sliderSound;
    [SerializeField]
    private SliderManager _sliderMusic;
    
    void Start()
    {
        _sliderSound.onValueChanged.AddListener(x=>((Source)global::DontDestroyOnLoad.instance).soundSource.volume=x);
        _sliderMusic.onValueChanged.AddListener(x=>((Source)global::DontDestroyOnLoad.instance).musicSource.volume=x);
    }
    
    
}
