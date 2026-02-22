using SadTabletop.Shared.Systems.Entities;

namespace SadTabletop.Shared.Systems.Assets;

public class AssetsSystem : EntitiesSystem<AssetInfo>
{
    public AssetsSystem(Game game) : base(game)
    {
    }

    public AssetInfo AddCardImageAsset(int num, string url)
    {
        return AddAsset($"card{num}", url, AssetVariant.Image);
    }

    public AssetInfo AddImageAsset(string name, string url)
    {
        return AddAsset(name, url, AssetVariant.Image);
    }

    public AssetInfo AddSoundAsset(string name, string url)
    {
        return AddAsset(name, url, AssetVariant.Sound);
    }

    public AssetInfo AddAsset(string name, string url, AssetVariant variant)
    {
        AssetInfo assetInfo = new AssetInfo(name, url, variant);

        this.AddEntity(assetInfo);

        return assetInfo;
    }
}