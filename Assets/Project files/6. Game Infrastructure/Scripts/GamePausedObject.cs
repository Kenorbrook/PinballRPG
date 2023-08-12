using System;
using UnityEngine;

public abstract class GamePausedObject : MonoBehaviour
{
    protected static bool isPause;

    private static Action pause;
    private static Action unPause;

    private void OnEnable()
    {
       pause += ConfirmPause;
       unPause += ConfirmUnPause;
    }

    private void OnDisable()
    {
        pause -= ConfirmPause;
        unPause -= ConfirmUnPause;
    }

    public static void Pause()
    {
        if (isPause) return;
        isPause = true;
        pause.Invoke();
    }

    public static void TogglePause()
    {
        if (isPause) UnPause();
        else Pause();
    }

    public static void UnPause()
    {
        if(!isPause) return;
        isPause = false;
        unPause.Invoke();
    }
    

    protected abstract void ConfirmPause();
    protected abstract void ConfirmUnPause();
}