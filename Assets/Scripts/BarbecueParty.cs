using UnityEngine;

public class BarbecueParty : MiniGameBase
{
    public override void Initialize()
    {
        base.Initialize();
        EventManager.OnBarbequeOpen += MiniGameOpen;
        EventManager.OnBarbequeClose += ResetMiniGame;
        EventManager.OnBarbequePlay += PlayGlowSequence;
    }

    public override void Dispose()
    {
        EventManager.OnBarbequeOpen -= MiniGameOpen;
        EventManager.OnBarbequeClose -= ResetMiniGame;
        EventManager.OnBarbequePlay -= PlayGlowSequence;
        base.Dispose();
    }
}
