using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameOverUI : CommonUI
{
    [Header("Buttons")]
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _goToMenuButton;

    public event Action GameWasRestarted;
    public event Action GoToMenuWasRequested;

    protected override void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _playAgainButton.onClick.AddListener(PlayAgain);
        _goToMenuButton.onClick.AddListener(GoToMenu);

        SetCanvasGroupValues(0, false, false);
    }

    public void PlayAgain()
    {
        GameWasRestarted?.Invoke();
        SetCanvasGroupValues(0, false, false);
    }

    private void GoToMenu()
    {
        GoToMenuWasRequested?.Invoke();
        SetCanvasGroupValues(0, false, false);
    }
}

