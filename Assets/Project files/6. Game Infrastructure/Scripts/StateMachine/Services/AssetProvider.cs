using UnityEngine;
using Object = UnityEngine.Object;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, GameObject at)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, at.transform.position, Quaternion.identity);
    }

}