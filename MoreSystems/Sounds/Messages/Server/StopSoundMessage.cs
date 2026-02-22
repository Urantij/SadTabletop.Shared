using SadTabletop.Shared.Systems.Communication;

namespace SadTabletop.Shared.MoreSystems.Sounds.Messages.Server;

public class StopSoundMessage(int id) : ServerMessageBase
{
    public int Id { get; } = id;
}