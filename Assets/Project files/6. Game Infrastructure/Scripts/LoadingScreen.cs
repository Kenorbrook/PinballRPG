using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvas;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvas.alpha = 1;
    }

    public void Hide()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_canvas.alpha>0)
        {
            _canvas.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }
        gameObject.SetActive(false);
    }
}
