using SadTabletop.Shared.Systems.Communication;

namespace SadTabletop.Shared.MoreSystems.Sounds.Messages.Server;

public class PlaySoundMessage(string assetName, float? multiplier, int? playId) : ServerMessageBase
{
    public string AssetName { get; } = assetName;
    public float? Multiplier { get; } = multiplier;
    public int? PlayId { get; } = playId;
}