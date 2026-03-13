using SadTabletop.Shared.Systems.Entities;

namespace SadTabletop.Shared.EvenMoreSystems.Menu;

public class MenuListsSystem : EntitiesSystem<MenuList>
{
    public MenuListsSystem(Game game) : base(game)
    {
    }

    public MenuList CreateMenuList(List<MenuButton> buttons)
    {
        MenuList list = new(buttons);
        AddEntity(list);

        return list;
    }
}