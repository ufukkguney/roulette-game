public class BarbecueParty : MiniGameBase
{
    public override void Initialize()
    {
        base.Initialize();
        EventManager.OnBarbequeOpen += MiniGameOpen;
        EventManager.OnBarbequeClose += ResetMiniGame;
        EventManager.OnBarbequePlay += () => SelectAndPlaySequence();
    }

    public override void Dispose()
    {
        EventManager.OnBarbequeOpen -= MiniGameOpen;
        EventManager.OnBarbequeClose -= ResetMiniGame;
        EventManager.OnBarbequePlay -= () => SelectAndPlaySequence();
        base.Dispose();
    }

    // public override void SelectAndPlaySequence(Action onSelectComplete = null)
    // {
    //     base.SelectAndPlaySequence(onSelectComplete);
    // }

    // private void OnSelectAnimComplete()
    // {
    //     // Handle the completion of the selection animation
    //     Debug.LogError("Selection animation completed");
    // }
}
