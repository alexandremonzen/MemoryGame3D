using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuUI : CommonUI
{
    [SerializeField] private GameObject _scenarioMenu;
    
    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButon;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;

    [Header("Screens")]
    [SerializeField] private GameObject[] _otherScreens;

    public event Action GameWasStarted;

    protected override void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _playButton.onClick.AddListener(PlayGame);
        _optionsButon.onClick.AddListener(() => ActivateScreen(0));
        _creditsButton.onClick.AddListener(() => ActivateScreen(1));
        _quitButton.onClick.AddListener(QuitApplication);
    }

    private void PlayGame()
    {
        SetCanvasGroupValues(0, false, false);
        _otherScreens[0].SetActive(false);
        SetMainMenuScenario(false, 1.15f);
        GameWasStarted?.Invoke();
    }

    private void ActivateScreen(int screenIndex)
    {
        bool screenWereActivated = _otherScreens[screenIndex].gameObject.activeSelf;
        DesactivateAllOtherScreens();

        if(!screenWereActivated)
            _otherScreens[screenIndex].SetActive(true);
    }

    private void QuitApplication()
    {
        Application.Quit();
    }

    private void DesactivateAllOtherScreens()
    {
        foreach(GameObject gameObject in _otherScreens)
            gameObject.SetActive(false);
    }

    public void SetMainMenuScenario(bool condition, float delay = 0)
    {
        if(delay > 0)
        {
            StartCoroutine(DelaySetCanvas(condition, delay));
            return;
        }

        _scenarioMenu.SetActive(condition);
    }

    private IEnumerator DelaySetCanvas(bool condition, float time)
    {
        yield return new WaitForSeconds(time);
        SetMainMenuScenario(condition);
    }
    
}
