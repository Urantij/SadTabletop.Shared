using SadTabletop.Shared.MoreSystems.Cards;

namespace SadTabletop.Shared.EvenMoreSystems.Chat.Embeds;

public class ChatEmbedCard(int contentIndex, CardFaceComplicated front, CardFaceComplicated back)
    : ChatEmbedBase(contentIndex)
{
    public CardFaceComplicated Front { get; } = front;
    public CardFaceComplicated Back { get; } = back;
}