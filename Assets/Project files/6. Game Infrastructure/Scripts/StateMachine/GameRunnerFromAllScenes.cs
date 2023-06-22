using UnityEngine;

public class GameRunnerFromAllScenes : MonoBehaviour
{
    [SerializeField]
    private GameBootstrapper _bootstrapperPrefab;
    #if UNITY_EDITOR
    private void Awake()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if (bootstrapper == null)
            Instantiate(_bootstrapperPrefab);
    }
    #endif
}
