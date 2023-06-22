using System.Collections.Generic;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;

public class LevelConstructFactory : ILevelConstructFactory
{
    public readonly List<Level> _currentLevels = new List<Level>();
    private readonly IAssetProvider _assetProvider;
    
    

    private Level lastLevel =>_currentLevels.Count > 0 ? _currentLevels[_currentLevels.Count - 1] : null;

    
    public LevelConstructFactory( IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public void CreateTutorialLevels(Transform parent)
    {
        var levels = _assetProvider.CreateTutorialLevels(parent: parent, lastLevel);
        foreach (var level in levels)
        {
            _currentLevels.Add(level);
        }
    }

    public void CreateStartLevel(Transform parent)
    {
        _currentLevels.Add(_assetProvider.InstantiateStartLevel(parent));
    }

    public void CreateRandomLevel(Transform parent)
    {
        _currentLevels.Add(_assetProvider.CreateRandomLevel(parent,lastLevel));
    }

    public void CreateRandomBossLevel(Transform parent)
    {
        
        _currentLevels.Add(_assetProvider.CreateRandomBossLevel(parent,lastLevel));
    }
}