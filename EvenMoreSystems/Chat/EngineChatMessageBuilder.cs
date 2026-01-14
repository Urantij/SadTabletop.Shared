using SadTabletop.Shared.EvenMoreSystems.Chat.Embeds;
using SadTabletop.Shared.MoreSystems.Cards;
using SadTabletop.Shared.Systems.Seats;

namespace SadTabletop.Shared.EvenMoreSystems.Chat;

public class EngineChatMessageBuilder
{
    private readonly List<string> _contentParts = [];
    private readonly List<ChatEmbedBase> _embeds = [];
    private int _nextIndex = 0;

    public EngineChatMessageBuilder Text(string content)
    {
        _contentParts.Add(content);

        _nextIndex += content.Length;

        return this;
    }

    public EngineChatMessageBuilder Card(CardFaceComplicated front, CardFaceComplicated back)
    {
        // TODO а это копировать надо или оно не меняется?
        _embeds.Add(new ChatEmbedCard(_nextIndex, front.Clowne(), back.Clowne()));

        _nextIndex++;

        return this;
    }

    public EngineChatMessageBuilder Seat(Seat seat)
    {
        // TODO ааа сидения пропадать умеют?
        _embeds.Add(new ChatEmbedSeat(_nextIndex, seat));

        _nextIndex++;

        return this;
    }

    public EngineChatMessage Build()
    {
        string content = String.Concat(_contentParts);

        return new EngineChatMessage(content, _embeds);
    }

    public static EngineChatMessageBuilder Make()
    {
        return new EngineChatMessageBuilder();
    }
}