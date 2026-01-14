namespace SadTabletop.Shared.EvenMoreSystems.Chat.Embeds;

public class ChatEmbedCustom(int contentIndex, string text, string color) : ChatEmbedBase(contentIndex)
{
    public string Text { get; } = text;
    public string Color { get; } = color;
}