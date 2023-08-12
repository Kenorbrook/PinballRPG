using UnityEngine;

public interface IAssetProvider : IService
{
    GameObject Instantiate(string path, GameObject at);
    GameObject Instantiate(string path);
}