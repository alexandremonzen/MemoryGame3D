using System;

using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    private MainMenuUI _mainMenu;
    private GameOverUI _gameOverUI;

    private PuzzleMemoryGame _puzzleMemoryGame;

    public event Action GameWasStarted;
    public event Action GameWasRestarted;
    public event Action SendToMenuWasRequested;

    private void Awake()
    {
        _mainMenu = FindObjectOfType<MainMenuUI>();
        _gameOverUI = FindObjectOfType<GameOverUI>();
        _puzzleMemoryGame = FindObjectOfType<PuzzleMemoryGame>();
    }

    private void OnEnable()
    {
        _mainMenu.GameWasStarted += () => InvokeAction(GameWasStarted);
        _mainMenu.GameWasStarted += StartMemoryGame;

        _gameOverUI.GameWasRestarted += () => InvokeAction(GameWasRestarted);
        _gameOverUI.GameWasRestarted += StartMemoryGame;

        _gameOverUI.GoToMenuWasRequested += () => InvokeAction(SendToMenuWasRequested);
        _gameOverUI.GoToMenuWasRequested += () => _mainMenu.SetCanvasGroupValues(1, true, true, 1.15f);
        _gameOverUI.GoToMenuWasRequested += () => _mainMenu.SetMainMenuScenario(true);

        _puzzleMemoryGame.GameWasCompleted += () => _gameOverUI.SetCanvasGroupValues(1, true, true, 0.75f);
    }

    private void OnDisable()
    {
        _mainMenu.GameWasStarted -= () => InvokeAction(GameWasStarted);
        _mainMenu.GameWasStarted -= StartMemoryGame;

        _gameOverUI.GameWasRestarted -= () => InvokeAction(GameWasRestarted);
        _gameOverUI.GameWasRestarted -= StartMemoryGame;

        _gameOverUI.GoToMenuWasRequested -= () => InvokeAction(SendToMenuWasRequested);
        _gameOverUI.GoToMenuWasRequested -= () => _mainMenu.SetCanvasGroupValues(1, true, true, 1.15f);
        _gameOverUI.GoToMenuWasRequested -= () => _mainMenu.SetMainMenuScenario(true);

        _puzzleMemoryGame.GameWasCompleted -= () => _gameOverUI.SetCanvasGroupValues(1, true, true, 0.75f);
    }

    private void InvokeAction(Action action)
    {
        action?.Invoke();
    }

    private void StartMemoryGame()
    {
        _puzzleMemoryGame.StartGame();
    }
}
