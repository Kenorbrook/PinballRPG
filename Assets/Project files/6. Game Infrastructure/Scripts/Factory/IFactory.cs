using UnityEngine;

public interface IFactory : IService
{
    void CreatePlayer(GameObject at);
    void CreateHud();

}