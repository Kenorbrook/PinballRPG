using ProjectFiles.Player;
using UnityEngine;

public class LevelFactory : IFactory
{
    private readonly IAssetProvider _assetProvider;
    
    public LevelFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public Player CreatePlayer(GameObject at)
    {
        Player.player = _assetProvider.Instantiate(path: PathResources.PLAYER, at).GetComponent<Player>();
        Player.player.SpawnPoint = at;
        return Player.player;
    }

    public GameObject CreateHud()
    {
        return _assetProvider.Instantiate(path: PathResources.INTERFACE);
    } 
    public GameObject CreateSkillData()
    {
        return _assetProvider.Instantiate(path: PathResources.SKILL_DATA);
    }
    
}