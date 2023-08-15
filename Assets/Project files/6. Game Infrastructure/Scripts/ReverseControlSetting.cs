using ProjectFiles.LevelInfrastructure;
using UnityEngine;
using UnityEngine.UI;

public class ReverseControlSetting : MonoBehaviour
{
    private static bool IsReverseControl
    {
        get => bool.Parse(PlayerPrefs.GetString("ReverseControl", false.ToString()));
        set => PlayerPrefs.SetString("ReverseControl", value.ToString());
    }
    [SerializeField]
    private Toggle _reverseControl;

    [SerializeField]
    private Text _reverseControlLabel;
    // Start is called before the first frame update
    void Start()
    {
        InitSetting();
    }
    public void ReverseControl(bool isOn)
    {
       // _reverseControlLabel.text = isOn ? "On" : "Off";
        IsReverseControl = isOn;
        GameManager.isReverse = isOn;
    }
    private void InitSetting()
    {
        _reverseControl.isOn = IsReverseControl;
        ReverseControl( _reverseControl.isOn);
    }
}
