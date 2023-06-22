using ProjectFiles.LevelInfrastructure;
using UnityEngine;

public interface ILevelConstructFactory: IService
{
    void CreateTutorialLevels(Transform parent);
    void CreateStartLevel(Transform parent);
    public void CreateRandomLevel(Transform parent);
    public void CreateRandomBossLevel(Transform parent);
}