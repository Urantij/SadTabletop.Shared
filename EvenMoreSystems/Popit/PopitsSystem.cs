using SadTabletop.Shared.EvenMoreSystems.Popit.Messages.Client;
using SadTabletop.Shared.Systems.Communication.Events;
using SadTabletop.Shared.Systems.Entities;
using SadTabletop.Shared.Systems.Events;
using SadTabletop.Shared.Systems.Seats;
using SadTabletop.Shared.Systems.Viewer;
using SadTabletop.Shared.Systems.Visability;

namespace SadTabletop.Shared.EvenMoreSystems.Popit;

public class PopitsSystem : EntitiesSystem<Popit>
{
    private readonly EventsSystem _events;
    private readonly VisabilitySystem _visability;
    private readonly ViewerSystem _viewer;

    public PopitsSystem(Game game) : base(game)
    {
    }

    protected internal override void GameCreated()
    {
        base.GameCreated();

        _events.Subscribe<ClientMessageReceivedEvent<ChoosePopitMessage>>(EventPriority.Normal, this, ChoiceWasMade);
    }

    protected internal override void GameLoaded()
    {
        base.GameLoaded();

        _viewer.RegisterEntity<Popit>(Transform);
    }

    public Popit GivePopit(string title, string[] options, Seat target, Action<Popit, int?> action,
        bool canSkip = false)
    {
        Popit popit = new(title, options, canSkip, action);
        _visability.HideFromEveryoneExcept(popit, target, sendRelatedMessage: false);

        AddEntity(popit);

        return popit;
    }

    private void ChoiceWasMade(ClientMessageReceivedEvent<ChoosePopitMessage> obj)
    {
        if (obj.Message.Choice != null)
        {
            if (obj.Message.Choice < 0 || obj.Message.Choice >= obj.Message.Popit.Options.Length)
            {
                // TODO warn
                return;
            }
        }
        else if (!obj.Message.Popit.CanSkip)
        {
            // TODO warn
            return;
        }

        // в теории злой клиент может угадать айди попыта и украсть инпут?..
        if (!_visability.IsVisibleFor(obj.Message.Popit, obj.Seat))
        {
            // TODO warn
            return;
        }

        RemoveEntity(obj.Message.Popit, sendRelatedMessage: false);

        obj.Message.Popit.Delegate(obj.Message.Popit, obj.Message.Choice);
    }

    private PopitDto Transform(Popit popit, Seat? seat)
    {
        return new PopitDto(popit);
    }
}