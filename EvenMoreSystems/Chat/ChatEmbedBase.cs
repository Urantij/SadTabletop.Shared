namespace SadTabletop.Shared.EvenMoreSystems.Chat;

/// <summary>
/// Кусок информации, который нужно встроить в сообщение в чате
/// </summary>
public abstract class ChatEmbedBase
{
    /// <summary>
    /// В каком месте контента находится эмбед
    /// </summary>
    public int ContentIndex { get; }

    protected ChatEmbedBase(int contentIndex)
    {
        ContentIndex = contentIndex;
    }
}