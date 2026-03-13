using SadTabletop.Shared.Mechanics;

namespace SadTabletop.Shared.EvenMoreSystems.Menu;

/// <summary>
/// Шаблон набора кнопок для <see cref="Menu"/>
/// </summary>
public class MenuList(List<MenuButton> buttons) : EntityBase
{
    public List<MenuButton> Buttons { get; } = buttons;
}