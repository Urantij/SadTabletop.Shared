using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.Systems.Table;

namespace SadTabletop.Shared.MoreSystems.Shapes;

public class ShapesSystem : SystemBase
{
    private readonly TableSystem _table;

    public ShapesSystem(Game game) : base(game)
    {
    }

    public RectShape AddRect(int x, int y, int width, int height, int color)
    {
        RectShape rect = new(x, y, width, height, color);

        _table.AddEntity(rect);

        return rect;
    }

    public CircleShape AddCircle(int x, int y, int radius, int color)
    {
        CircleShape circle = new(x, y, radius, color);

        _table.AddEntity(circle);

        return circle;
    }
}