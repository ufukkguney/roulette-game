using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour, IDisposable
{
    [SerializeField] private Button miniGameButton;

    public void Initialize()
    {
        miniGameButton.onClick.AddListener(OnMiniGameButtonClicked);
    }

    private void OnMiniGameButtonClicked()
    {
        EventManager.InvokeBarbequeOpen();
    }

    public void Dispose()
    {
        miniGameButton.onClick.RemoveListener(OnMiniGameButtonClicked);
    }
}
