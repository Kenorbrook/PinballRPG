using ProjectFiles.Player;
using UnityEngine;

public interface IFactory : IService
{
    Player CreatePlayer(GameObject at);
    GameObject CreateHud();
    GameObject CreateSkillData();

}