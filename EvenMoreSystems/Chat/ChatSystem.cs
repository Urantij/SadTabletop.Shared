using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.Systems.Seats;

namespace SadTabletop.Shared.EvenMoreSystems.Chat;

/// <summary>
/// Не совсем "чат", но система позволяет отправлять сообщения определённым местами
/// </summary>
public class ChatSystem : SystemBase
{
    // как будто можно через комуникейшн делать, но можно и не делать)
    public event Action<EngineChatMessage, IReadOnlyList<Seat>?>? ChatMessageSendRequested;

    public ChatSystem(Game game) : base(game)
    {
    }

    public void SendMessage(Seat target, string content)
    {
        EngineChatMessage msg = new(content, []);

        SendMessage(target, msg);
    }

    public void SendMessage(Seat target, EngineChatMessageBuilder builder)
    {
        EngineChatMessage msg = builder.Build();

        SendMessage(target, msg);
    }

    public void SendMessage(Seat target, EngineChatMessage msg)
    {
        ChatMessageSendRequested?.Invoke(msg, [target]);
    }
}