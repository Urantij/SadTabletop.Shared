using SadTabletop.Shared.Mechanics;
using SadTabletop.Shared.MoreSystems.Sounds.Messages.Server;
using SadTabletop.Shared.Systems.Assets;
using SadTabletop.Shared.Systems.Communication;

namespace SadTabletop.Shared.MoreSystems.Sounds;

// значица, я хочу возможность управлять звуками на клиенте
// в тевории самое простое это шерить список ассетов и по команде их запускать..
// но какие ещё могут быть нюансы... надо подумать...
// громкость? вот тут нормализация громкость и прочее это оч оч сложно
// пока думать не буду, сделаю просто float с множителем громкости наверное

// возможность остановить проигрывание было бы интересно.
// клиенту отправлять назад айди играющего звука звучит супер мега впадлу.
// пусть сервер придумывает айди для таких звуков. и сам следит, чтобы они были уникальные для клиента.
// тут возникает проблема айдишников - если айди звуков общие между клиентами,
// по номеру айди можно понять, играло ли звуки до этого.
// TODO можно двигать айди вперёд на случайное значение. и начинать не с 0. или с 0, но рандомно, не всегда

/// <summary>
/// Позволяет управлять звуками на клиенте.
/// </summary>
public class SoundsSystem : SystemBase
{
    private readonly CommunicationSystem _communication;

    private int _nextPlayId = 1;

    public SoundsSystem(Game game) : base(game)
    {
    }

    /// <summary>
    /// Проиграть звук для всех
    /// </summary>
    public void PlaySound(AssetInfo assetInfo, float? multiplier = null)
    {
        PlaySoundMessage message = new(assetInfo.Name, multiplier, null);
        _communication.Send(message);
    }

    public SoundRemote PlayControllableSound(AssetInfo assetInfo, float? multiplier = null)
    {
        int playId = GetNextPlayId();

        SoundRemote remote = new(playId, null);

        PlaySoundMessage message = new(assetInfo.Name, multiplier, playId);
        _communication.Send(message);

        return remote;
    }

    public void StopSound(SoundRemote remote)
    {
        StopSoundMessage message = new(remote.Id);

        if (remote.Listeners != null)
        {
            _communication.Send(message, remote.Listeners);
        }
        else
        {
            _communication.Send(message);
        }
    }

    private int GetNextPlayId()
    {
        return _nextPlayId++;
    }
}