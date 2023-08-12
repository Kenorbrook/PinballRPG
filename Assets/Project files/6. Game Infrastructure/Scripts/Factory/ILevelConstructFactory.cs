using ProjectFiles.LevelInfrastructure;
using UnityEngine;

public interface ILevelConstructFactory: IService
{
    int GetLevelCount();
    void CreateTutorialLevels(Transform parent);
    void CreateStartLevel(Transform parent);
    public void CreateRandomLevel(Transform parent);
    public void CreateRandomBossLevel(Transform parent, BossInterface bossInterface);
    public Level GetCurrentLevel();
}