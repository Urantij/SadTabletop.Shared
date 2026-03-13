using SadTabletop.Shared.Systems.Seats;

namespace SadTabletop.Shared.EvenMoreSystems.Menu.Actions;

public delegate void SendServerReceived(Seat seat);

public class SendServerMenuAction(int serverId) : MenuActionBase
{
    public int ServerId { get; } = serverId;
}