using UnityEngine;

public class GameRunnerFromAllScenes : MonoBehaviour
{
    [SerializeField]
    private GameBootstrapper _bootstrapperPrefab;
    private void Awake()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if (bootstrapper == null)
            Instantiate(_bootstrapperPrefab);
    }
}
