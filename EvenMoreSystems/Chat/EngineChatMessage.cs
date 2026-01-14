namespace SadTabletop.Shared.EvenMoreSystems.Chat;

/// <summary>
/// Сообщение, которое хочет скинуть игра.
/// </summary>
public class EngineChatMessage
{
    public string Content { get; }
    public IReadOnlyList<ChatEmbedBase> Embeds { get; }

    internal EngineChatMessage(string content, IReadOnlyList<ChatEmbedBase> embeds)
    {
        Content = content;
        Embeds = embeds;
    }
}