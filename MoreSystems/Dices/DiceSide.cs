namespace SadTabletop.Shared.MoreSystems.Dices;

public class DiceSide(int data, string? display, int? assetId)
{
    /// <summary>
    /// Серверная информация для определения стороны
    /// </summary>
    public int Data { get; } = data;

    /// <summary>
    /// Рисуется на клиенте, если есть
    /// </summary>
    public string? Display { get; } = display;

    /// <summary>
    /// Айди картинки стороны
    /// </summary>
    public int? AssetId { get; } = assetId;
}

public class DiceSideDto(DiceSide diceSide)
{
    /// <summary>
    /// Рисуется на клиенте, если есть
    /// </summary>
    public string? Display { get; } = diceSide.Display;

    /// <summary>
    /// Айди картинки стороны
    /// </summary>
    public int? AssetId { get; } = diceSide.AssetId;
}