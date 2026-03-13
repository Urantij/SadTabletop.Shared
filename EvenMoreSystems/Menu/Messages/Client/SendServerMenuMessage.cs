using SadTabletop.Shared.Systems.Communication;

namespace SadTabletop.Shared.EvenMoreSystems.Menu.Messages.Client;

public class SendServerMenuMessage(int serverId) : ClientMessageBase
{
    public int ServerId { get; } = serverId;
}