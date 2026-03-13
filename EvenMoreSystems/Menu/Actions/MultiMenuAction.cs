namespace SadTabletop.Shared.EvenMoreSystems.Menu.Actions;

public class MultiMenuAction(List<MenuActionBase> subActions) : MenuActionBase
{
    public List<MenuActionBase> SubActions { get; } = subActions;
}