using SadTabletop.Shared.Mechanics;

namespace SadTabletop.Shared.Systems.Assets;

public class AssetInfo(string name, string url, AssetVariant variant) : EntityBase
{
    public string Name { get; } = name;
    public string Url { get; } = url;
    public AssetVariant Variant { get; } = variant;
}