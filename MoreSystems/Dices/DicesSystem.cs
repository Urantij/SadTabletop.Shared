using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.MoreSystems.Dices.Messages;
using SadTabletop.Shared.Systems.Assets;
using SadTabletop.Shared.Systems.Communication;
using SadTabletop.Shared.Systems.Events;
using SadTabletop.Shared.Systems.Limit;
using SadTabletop.Shared.Systems.Limit.Events;
using SadTabletop.Shared.Systems.MyRandom;
using SadTabletop.Shared.Systems.Seats;
using SadTabletop.Shared.Systems.Table;
using SadTabletop.Shared.Systems.Viewer;

namespace SadTabletop.Shared.MoreSystems.Dices;

public class DicesSystem : SystemBase
{
    private readonly LimitSystem _limit;
    private readonly EventsSystem _events;
    private readonly ViewerSystem _viewer;
    private readonly RandomSystem _random;
    private readonly CommunicationSystem _communication;

    private readonly TableSystem _table;

    public DicesSystem(Game game) : base(game)
    {
    }

    protected internal override void GameCreated()
    {
        base.GameCreated();

        _events.Subscribe<LimitedEvent>(EventPriority.Normal, this, Limited);
    }

    protected internal override void GameLoaded()
    {
        base.GameLoaded();

        _viewer.RegisterEntity<Dice>(TransformDice);
    }

    public Dice CreateSimpleDice(float x, float y, int[] sideValues, AssetInfo? defaultAsset, int currentIndex = 0,
        bool sendRelatedMessage = true)
        => CreateSimpleDice(x, y, sideValues, defaultAsset?.Id, currentIndex, sendRelatedMessage);

    public Dice CreateSimpleDice(float x, float y, int[] sideValues, int? defaultAssetId, int currentIndex = 0,
        bool sendRelatedMessage = true)
    {
        Dice dice = new(sideValues.Select(sv => new DiceSide(sv, sv.ToString(), null)).ToArray(), defaultAssetId)
        {
            X = x,
            Y = y,
            CurrentSideIndex = currentIndex
        };

        _table.AddEntity(dice, sendRelatedMessage: sendRelatedMessage);

        return dice;
    }

    public Dice CreateDice(float x, float y, DiceSide[] sides, AssetInfo? defaultAsset, int currentIndex = 0,
        bool sendRelatedMessage = true)
        => CreateDice(x, y, sides, defaultAsset?.Id, currentIndex, sendRelatedMessage);

    public Dice CreateDice(float x, float y, DiceSide[] sides, int? defaultAssetId, int currentIndex = 0,
        bool sendRelatedMessage = true)
    {
        Dice dice = new(sides, defaultAssetId)
        {
            X = x,
            Y = y,
            CurrentSideIndex = currentIndex
        };
        _table.AddEntity(dice, sendRelatedMessage: sendRelatedMessage);

        return dice;
    }

    // Разные методы, так как не уверен, мб стоит разные сообщения сделать.
    public void Set(Dice dice, int newIndex)
    {
        dice.CurrentSideIndex = newIndex;

        (Seat?[] theyKnow, Seat?[] theyDontKnow) a = _limit.FindWitnesses(dice);

        _communication.SendEntityRelated(new DiceRolledMessage(dice, newIndex), dice, a.theyKnow);
        _communication.SendEntityRelated(new DiceRolledMessage(dice, null), dice, a.theyDontKnow);
    }

    public void Roll(Dice dice)
    {
        int newIndex = _random.Get(0, dice.Sides.Count);

        dice.CurrentSideIndex = newIndex;

        (Seat?[] theyKnow, Seat?[] theyDontKnow) a = _limit.FindWitnesses(dice);

        _communication.SendEntityRelated(new DiceRolledMessage(dice, newIndex), dice, a.theyKnow);
        _communication.SendEntityRelated(new DiceRolledMessage(dice, null), dice, a.theyDontKnow);
    }

    private void Limited(LimitedEvent obj)
    {
        if (obj.Entity is not Dice dice)
            return;

        if (obj.TheyKnowNow != null)
        {
            _communication.SendEntityRelated(new DiceInfoMessage(dice, dice.CurrentSideIndex), dice, obj.TheyKnowNow);
        }

        if (obj.TheyDontKnowNow != null)
        {
            _communication.SendEntityRelated(new DiceInfoMessage(dice, null), dice, obj.TheyDontKnowNow);
        }
    }

    private DiceDto TransformDice(Dice dice, Seat? target)
    {
        int? index;

        if (_limit.IsLimitedFor(dice, target))
        {
            index = null;
        }
        else
        {
            index = dice.CurrentSideIndex;
        }

        return new DiceDto(dice, index);
    }
}