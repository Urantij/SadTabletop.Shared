namespace SadTabletop.Shared.EvenMoreSystems.Menu.Actions;

public class ChangeListMenuAction(int menuTemplateId) : MenuActionBase
{
    public int MenuTemplateId { get; } = menuTemplateId;
}