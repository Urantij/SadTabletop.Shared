using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.Systems.Entities;
using SadTabletop.Shared.Systems.Seats;
using SadTabletop.Shared.Systems.Synchro;
using SadTabletop.Shared.Systems.Visability;

namespace SadTabletop.Shared.Systems.Viewer;

public delegate IEntity EntityViewerTransform<in T>(T thing, Seat? target);

public delegate IClientComponent? ComponentViewerTransform<in T>(T thing, Seat? target);

public class PointOfView(Type type, Delegate @delegate)
{
    public Type Type { get; } = type;
    public Delegate Delegate { get; } = @delegate;
}

/// <summary>
/// Когда внешнему миру нужно превратить модель сервера в модель для клиента, он обращается сюда.
/// Системы, ответственные за свои сущности, регистрируют трансформы здесь.
/// </summary>
public class ViewerSystem : SystemBase
{
    private readonly VisabilitySystem _visability;

    // TODO наверное не надо инклуд, пусть системы каждый раз регают, но нужно это как то вынести в описание.
    private readonly List<PointOfView> _entities = new();
    private readonly List<PointOfView> _components = new();

    public ViewerSystem(Game game) : base(game)
    {
    }

    public IEnumerable<ViewedEntity> ViewEntities(EntitiesSystem entitiesSystem, Seat? target)
    {
        if (!entitiesSystem.ClientSided)
            throw new Exception($"эта система не клиент сидед {entitiesSystem.GetType().Name}");

        return entitiesSystem.EnumerateRawEntities()
            .Where(e => _visability.IsVisibleFor(e, target))
            .Select(e => ViewEntity(e, target));
    }

    public ViewedEntity ViewEntity(EntityBase entity, Seat? target)
    {
        return new ViewedEntity(
            this.View(entity, target),
            entity.ReadClientComponents()
                .Select(c => this.View(c, target))
                .Where(c => c != null)
                .ToArray()
        );
    }

    public IEntity View(IEntity entity, Seat? seat)
    {
        // Если есть трансформ, применить вернуть. Иначе просто вернуть

        PointOfView? transform = _entities.FirstOrDefault(v => v.Type == entity.GetType());

        if (transform == null)
            return entity;

        return (IEntity)transform.Delegate.DynamicInvoke(entity, seat);
    }

    public IClientComponent? View(IClientComponent clientComponent, Seat? seat)
    {
        // Если есть трансформ, применить вернуть. Иначе просто вернуть

        PointOfView? transform = _components.FirstOrDefault(v => v.Type == clientComponent.GetType());

        if (transform == null)
            return clientComponent;

        return transform.Delegate.DynamicInvoke(clientComponent, seat) as IClientComponent;
    }

    public void RegisterEntity<T>(EntityViewerTransform<T> transform) where T : EntityBase
    {
        if (_entities.Any(o => o.Type == typeof(T)))
        {
            throw new Exception(
                $"Попытка добавить преобразователь для типа, который уже присутствует в коллекции типочков. {typeof(T).Name}");
        }

        _entities.Add(new PointOfView(typeof(T), transform));
    }

    public void RegisterComponent<T>(ComponentViewerTransform<T> transform)
        where T : ClientComponentBase, IClientComponent
    {
        if (_components.Any(o => o.Type == typeof(T)))
        {
            throw new Exception(
                $"Попытка добавить преобразователь для типа, который уже присутствует в коллекции типочков. {typeof(T).Name}");
        }

        _components.Add(new PointOfView(typeof(T), transform));
    }
}