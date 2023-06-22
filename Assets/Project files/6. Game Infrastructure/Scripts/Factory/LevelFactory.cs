using ProjectFiles.Player;
using UnityEngine;

public class LevelFactory : IFactory
{
    private readonly IAssetProvider _assetProvider;
    
    public LevelFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public void CreatePlayer(GameObject at)
    {
        Player.player = _assetProvider.Instantiate(path: PathResources.PLAYER, at).GetComponent<Player>();
        Player.player.SpawnPoint = at;
    }

    public void CreateHud()
    {
        _assetProvider.Instantiate(path: PathResources.INTERFACE);
    }
    
}