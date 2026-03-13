using SadTabletop.Shared.Mechanics;

namespace SadTabletop.Shared.EvenMoreSystems.Menu;

/// <summary>
/// Хранит информацию о меню. Какие менюшки.
/// </summary>
public class Menu(string title, List<MenuButton> buttons) : EntityBase
{
    public string Title { get; internal set; } = title;
    public List<MenuButton> Buttons { get; } = buttons;
}