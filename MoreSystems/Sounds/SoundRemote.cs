using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.Systems.Seats;

namespace SadTabletop.Shared.MoreSystems.Sounds;

public class SoundRemote(int id, Spisok<Seat?>? listeners)
{
    public int Id { get; } = id;

    /// <summary>
    /// Если список нулл, все.
    /// </summary>
    public Spisok<Seat?>? Listeners { get; } = listeners;
}