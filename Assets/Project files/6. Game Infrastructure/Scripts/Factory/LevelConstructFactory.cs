using System.Collections.Generic;
using ProjectFiles.Enemies;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;

public class LevelConstructFactory : ILevelConstructFactory
{
    public readonly List<Level> currentLevels = new List<Level>();
    private readonly IAssetLevelProvider _assetLevelProvider;
    
    

    private Level lastLevel =>currentLevels.Count > 0 ? currentLevels[currentLevels.Count - 1] : null;


    
    public LevelConstructFactory( IAssetLevelProvider assetProvider)
    {
        _assetLevelProvider = assetProvider;
    }

    public int GetLevelCount()=> currentLevels.Count;

    public void CreateTutorialLevels(Transform parent)
    {
        var levels = _assetLevelProvider.CreateTutorialLevels(parent: parent, lastLevel);
        foreach (var level in levels)
        {
            currentLevels.Add(level);
        }
    }

    public void CreateStartLevel(Transform parent)
    {
        currentLevels.Add(_assetLevelProvider.InstantiateStartLevel(parent));
    }

    public void CreateRandomLevel(Transform parent)
    {
        currentLevels.Add(_assetLevelProvider.CreateRandomLevel(parent,lastLevel));
    }

    public void CreateRandomBossLevel(Transform parent, BossInterface bossInterface)
    {
        Level bossLevel = _assetLevelProvider.CreateRandomBossLevel(parent, lastLevel);
        bossLevel.GetComponentInChildren<Boss>().@interface = bossInterface;
        currentLevels.Add(bossLevel);
    }

    public Level GetCurrentLevel() => currentLevels[currentLevels.Count - 1];
}