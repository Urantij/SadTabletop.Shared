namespace SadTabletop.Shared.EvenMoreSystems.Menu;

public class MenuButton(string text, MenuActionBase action, string? color = null)
{
    public string Text { get; } = text;
    public string? Color { get; } = color;
    public MenuActionBase Action { get; } = action;
}