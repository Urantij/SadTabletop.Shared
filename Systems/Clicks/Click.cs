using SadTabletop.Shared.Systems.Seats;
using SadTabletop.Shared.Systems.Table;

namespace SadTabletop.Shared.Systems.Clicks;

/// <summary>
/// Произошедший клик.
/// </summary>
public class Click(Seat? seat, TableItem item, ClickComponent component, float? x, float? y)
{
    public Seat? Seat { get; } = seat;
    public TableItem Item { get; } = item;
    public ClickComponent Component { get; } = component;
    public float? X { get; } = x;
    public float? Y { get; } = y;
}