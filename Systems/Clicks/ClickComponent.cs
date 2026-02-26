using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.Systems.Seats;
using SadTabletop.Shared.Systems.Viewer;

namespace SadTabletop.Shared.Systems.Clicks;

public class ClickComponent(Seat? seat, Action<Click> @delegate, bool singleUse, bool sendClickPosition)
    : ClientComponentBase
{
    public Seat? Seat { get; } = seat;
    public Action<Click> Delegate { get; } = @delegate;
    public bool SingleUse { get; } = singleUse;

    /// <summary>
    /// Должен ли клиент отправлять координаты клика.
    /// В координатах будет локальная позиция клика внутри объекта, где 0:0 это глобальная координата объекта на столе.
    /// </summary>
    public bool SendClickPosition { get; } = sendClickPosition;
}

public class ClickClientComponentDto(ClickComponent component) : ClientComponentDto(component)
{
    public Seat? Seat { get; } = component.Seat;
    public bool SingleUse { get; } = component.SingleUse;
    public bool SendClickPosition { get; } = component.SendClickPosition;

    public override Type WhatIsMyType() => typeof(ClickComponent);
}