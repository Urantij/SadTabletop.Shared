using SadTabletop.Shared.Systems.Limit;
using SadTabletop.Shared.Systems.Table;

namespace SadTabletop.Shared.MoreSystems.Dices;

/// <summary>
/// Дайс, у которого есть стороны, и который можно ролить.
/// </summary>
public class Dice(IReadOnlyList<DiceSide> sides, int? defaultAssetId) : TableItem, ILimitable
{
    public int CurrentSideIndex { get; set; }

    /// <summary>
    /// Должен быть хотя бы один элемент, иначе краш.
    /// </summary>
    public IReadOnlyList<DiceSide> Sides { get; } = sides;

    /// <summary>
    /// Отображаемая картинка по умолчанию.
    /// </summary>
    public int? DefaultAssetId { get; } = defaultAssetId;
}

public class DiceDto(Dice dice, int? currentSideIndex) : TableItemDto(dice)
{
    public int? CurrentSideIndex { get; } = currentSideIndex;

    public IReadOnlyList<DiceSideDto> Sides { get; } = dice.Sides.Select(d => new DiceSideDto(d)).ToArray();

    public int? DefaultAssetId { get; } = dice.DefaultAssetId;

    public override Type WhatIsMyType() => typeof(Dice);
}