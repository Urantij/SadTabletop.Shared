using SadTabletop.Shared.Systems.Seats;

namespace SadTabletop.Shared.EvenMoreSystems.Chat.Embeds;

public class ChatEmbedSeat(int contentIndex, Seat who) : ChatEmbedBase(contentIndex)
{
    public int WhoId { get; } = who.Id;
}