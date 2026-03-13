using SadTabletop.Shared.EvenMoreSystems.Menu.Actions;
using SadTabletop.Shared.EvenMoreSystems.Menu.Messages.Client;
using SadTabletop.Shared.Systems.Communication;
using SadTabletop.Shared.Systems.Communication.Events;
using SadTabletop.Shared.Systems.Entities;
using SadTabletop.Shared.Systems.Events;

namespace SadTabletop.Shared.EvenMoreSystems.Menu;

// ну, я просто не знаю, что делать, и хочу сделать хоть что то, потому что мне нечего делать
// значица, меню это просто окно, в котором есть кнопки, которые делают действия
// действия могут быть как сменить набор кнопок, так отправить действие на сервер, закрыть меню
// попытка сделать чуть менее сервер рендер. так что большинство действий нужно постараться сделать клиентными
// например смену кнопок.

// представим меню, в котором игрок может выбрать одну из трёх категорий, в этой категории уже действие или вернуться
// нажатие на категорию должно в этом же меню показать другой набор кнопок. а не показать новое меню.
// при этом я хочу, чтобы эти кнопки были заранее известны (наверное стоит добавить и опцию, чтобы кнопки приходили с сервера)
// если есть действие на заменю кнопки на кнопки, то подкатегория при попытке вернуться будет ссылаться на набор, где есть кнопка
// которая вызывает это подменю и будет бесконечный референс.
// сделать сериализацию десериализацию в такой системе возможно, но я чесно говоря заебауся
// тогда можно сделать MenuList который содержит кнопки. и действием будет вызов определённого листа,
// которые уже могут ссылаться друг на друга окей.
// получается, это темплейты... которые.. ну короче понятно

/// <summary>
/// Меню это окно, которое содержит кнопки, которые можно нажимать.
/// </summary>
public class MenuSystem : EntitiesSystem<Menu>
{
    private readonly EventsSystem _events;

    // TODO сериализация

    private int _nextActionId = 1;

    private readonly Dictionary<int, SendServerReceived> _sendDict = [];

    public MenuSystem(Game game) : base(game)
    {
    }

    protected internal override void GameCreated()
    {
        base.GameCreated();

        _events.Subscribe<ClientMessageReceivedEvent<SendServerMenuMessage>>(EventPriority.Normal, this,
            ClientActionReceived);
    }

    public Menu CreateMenu(string title, MenuList list)
    {
        Menu menu = new(title, list.Buttons.ToList());
        AddEntity(menu);

        return menu;
    }

    public Menu CreateMenu(string title, List<MenuButton> buttons)
    {
        Menu menu = new(title, buttons);
        AddEntity(menu);

        return menu;
    }

    /// <summary>
    /// Если надо, не забудь потом про <see cref="UnregisterSend"/>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public SendServerMenuAction RegisterSend(SendServerReceived action)
    {
        SendServerMenuAction menuAction = new(GetNextActionId());

        _sendDict[menuAction.ServerId] = action;

        return menuAction;
    }

    public void UnregisterSend(int id)
    {
        _sendDict.Remove(id);
    }

    private void ClientActionReceived(ClientMessageReceivedEvent<SendServerMenuMessage> obj)
    {
        if (!_sendDict.TryGetValue(obj.Message.ServerId, out var action) || obj.Seat == null)
        {
            // TODO варн
            return;
        }

        action(obj.Seat);
    }

    // TODO более рендом
    private int GetNextActionId()
    {
        return _nextActionId++;
    }
}