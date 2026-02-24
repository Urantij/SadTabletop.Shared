using SadTabletop.Shared.Systems.Communication;

namespace SadTabletop.Shared.MoreSystems.Sounds.Messages.Server;

public class PlaySoundMessage(string assetName, float? multiplier, int? playId, SoundCategory? category, bool? loop)
    : ServerMessageBase
{
    public string AssetName { get; } = assetName;
    public float? Multiplier { get; } = multiplier;

    public int? PlayId { get; } = playId;

    // TODO кабутабы можно не отправлять, если это дефолт, если это эффект
    public SoundCategory? category { get; } = category;

    // TODO сейм, можно не отправлять, если не тру
    public bool? Loop { get; } = loop;
}