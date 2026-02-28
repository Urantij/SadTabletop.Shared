using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.Systems.Viewer;

namespace SadTabletop.Shared.EvenMoreSystems.Popit;

public class Popit(string title, string[] options, bool canSkip, Action<Popit, int?> @delegate) : EntityBase
{
    public string Title { get; } = title;

    public string[] Options { get; } = options;

    /// <summary>
    /// Может ли клиент не выбирать из зол.
    /// </summary>
    public bool CanSkip { get; } = canSkip;

    public Action<Popit, int?> Delegate { get; } = @delegate;
}

public class PopitDto(Popit popit) : EntityBaseDto(popit)
{
    public string Title { get; } = popit.Title;
    public string[] Options { get; } = popit.Options;
    public bool CanSkip { get; } = popit.CanSkip;

    public override Type WhatIsMyType() => typeof(Popit);
}