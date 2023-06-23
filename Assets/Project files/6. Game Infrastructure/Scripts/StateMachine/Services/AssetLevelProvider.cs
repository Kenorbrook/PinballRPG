using System;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class AssetLevelProvider : IAssetLevelProvider
{
    private readonly Level[] _tutorialLevels = Resources.LoadAll<Level>("TutorialLevels");
    private readonly Level[] _allLevels = Resources.LoadAll<Level>("Levels");
    private readonly Level[] _allBosses = Resources.LoadAll<Level>("Bosses");
    private static Random Rand => new Random(DateTime.Now.Millisecond);

    public Level InstantiateLevel(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, parent).GetComponent<Level>();
    }

    public Level InstantiateLevel(string path, Vector3 transform, Quaternion quaternion, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, transform, quaternion,
            parent).GetComponent<Level>();
    }

    public Level[] CreateTutorialLevels(Transform parent, Level lastLevel)
    {
        Vector3 position = Vector3.zero;
        if (lastLevel)
        {
            position.y += lastLevel.transform.position.y + lastLevel.height;
        }

        Level[] _levels = new Level[_tutorialLevels.Length];
        for (var _indexLevel = 0; _indexLevel < _tutorialLevels.Length; _indexLevel++)
        {
            _levels[_indexLevel] = Object.Instantiate(_tutorialLevels[_indexLevel],
                position,
                new Quaternion(),
                parent);
            position.y = _levels[_indexLevel].transform.position.y + _levels[_indexLevel].height;
        }

        return _levels;
    }

    public Level InstantiateStartLevel(Transform parent)
    {
        return InstantiateLevel(PathResources.START_LEVEL, parent);
    }

    public Level CreateRandomLevel(Transform parent, Level lastLevel)
    {
        Vector3 position = Vector3.zero;
        if (lastLevel)
        {
            position.y += lastLevel.transform.position.y + lastLevel.height;
        }

        int _level = Rand.Next(0, _allLevels.Length);
        return Object.Instantiate(_allLevels[_level], position, new Quaternion(), parent).GetComponent<Level>();
    }

    public Level CreateRandomBossLevel(Transform parent, Level lastLevel)
    {
        Vector3 position = Vector3.zero;
        if (lastLevel)
        {
            position.y += lastLevel.transform.position.y + lastLevel.height;
        }

        int _level = Rand.Next(0, _allBosses.Length);
        return Object.Instantiate(_allBosses[_level], position, new Quaternion(), parent).GetComponent<Level>();
    }
}