using SadTabletop.Shared.Systems.Communication;

namespace SadTabletop.Shared.EvenMoreSystems.Popit.Messages.Client;

public class ChoosePopitMessage(Popit popit, int? choice) : ClientMessageBase
{
    public Popit Popit { get; } = popit;

    /// <summary>
    /// Индекс <see cref="EvenMoreSystems.Popit.Popit.Options"/>
    /// </summary>
    public int? Choice { get; } = choice;
}