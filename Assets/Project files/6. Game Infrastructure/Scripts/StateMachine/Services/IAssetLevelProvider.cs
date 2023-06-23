using ProjectFiles.LevelInfrastructure;
using UnityEngine;

public interface IAssetLevelProvider:IService
{
    Level InstantiateLevel(string path,  Transform parent);
    Level InstantiateLevel(string path, Vector3 transform, Quaternion quaternion, Transform parent);
    Level[] CreateTutorialLevels(Transform parent, Level lastLevel);
    Level InstantiateStartLevel(Transform parent);
    public Level CreateRandomLevel(Transform parent, Level lastLevel);
    public Level CreateRandomBossLevel(Transform parent, Level lastLevel);
}