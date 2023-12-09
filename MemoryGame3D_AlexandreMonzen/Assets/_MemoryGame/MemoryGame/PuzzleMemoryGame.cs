using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public sealed class PuzzleMemoryGame : MonoBehaviour
{
    [SerializeField] private bool _autoStartEditor = false;

    [Header("Time Setup")]
    [SerializeField] private float _intervalToUnflip = 1;
    [SerializeField] private float _intervalMatchedFeedback = 0.7f;
    [SerializeField] private float _intervalAllowFlip = 0.15f;
    private List<MemoryCard> _memoryCards;

    private int _pairsNumber;
    private int _totalPairsNumber;
    private int _resolvedPairs;

    private MemoryCard _firstMemoryCard;
    private MemoryCard _secondMemoryCardTwo;

    private MemoryCard _randomMemoryCard;
    private Vector3 _previousMemoryCardPosition;

    private MemoryCardsPlacement _cardsPlacement;

    public event Action<bool> CanFlipCardWasChanged;
    public event Action GameWasCompleted;

    private void Awake()
    {
        _cardsPlacement = GetComponent<MemoryCardsPlacement>();
        _memoryCards = new List<MemoryCard>();

        #if UNITY_EDITOR
        if (_autoStartEditor)
            StartGame();
        #endif
    }

    public void StartGame()
    {
        SetupGame();
        SetCardsValue();
        ShuffleMemoryCards();
    }

    private void SetupGame()
    {
        if(!_autoStartEditor) 
            _memoryCards = _cardsPlacement.CreateCardsInGameplay();
        
        GetComponentsInChildren(_memoryCards);

        if (_memoryCards.Count % 2 != 0)
        {
            this.gameObject.SetActive(false);
            CanFlipCardWasChanged?.Invoke(false);
            Debug.LogError("Aborting puzzle setup, the number of memoryCards must match a pair number");
            return;
        }

        _totalPairsNumber = _memoryCards.Count / 2;
        _resolvedPairs = 0;
        CanFlipCardWasChanged?.Invoke(false);
    }

    public void SetCardsValue()
    {
        _pairsNumber = 0;

        for (int i = 0; i < _memoryCards.Count; i++)
        {
            _memoryCards[i].gameObject.SetActive(true);
            _memoryCards[i].SetValueCard(_pairsNumber);
            if (i % 2 != 0) _pairsNumber++;
        }
    }

    public void ShuffleMemoryCards()
    {
        foreach (MemoryCard memoryCard in _memoryCards)
        {
            _randomMemoryCard = _memoryCards[UnityEngine.Random.Range(0, _memoryCards.Count)];
            _previousMemoryCardPosition = memoryCard.transform.position;

            memoryCard.transform.position = _randomMemoryCard.transform.position;
            _randomMemoryCard.transform.position = _previousMemoryCardPosition;
        }

        _randomMemoryCard = null;
        CanFlipCardWasChanged?.Invoke(true);
    }

    public void UpdateActualCards(MemoryCard memoryCard)
    {
        if (_firstMemoryCard == null)
        {
            _firstMemoryCard = memoryCard;
            return;
        }

        if (_firstMemoryCard == memoryCard)
            return;

        _secondMemoryCardTwo = memoryCard;
        CanFlipCardWasChanged?.Invoke(false);

        StartCoroutine(CheckCardsOpen());
    }

    private IEnumerator CheckCardsOpen()
    {
        if (_firstMemoryCard.CardValue != _secondMemoryCardTwo.CardValue)
        {
            yield return new WaitForSeconds(_intervalToUnflip);
            _firstMemoryCard.UnFlipCard();
            _secondMemoryCardTwo.UnFlipCard();
        }
        else
        {
            yield return new WaitForSeconds(_intervalMatchedFeedback);
            _firstMemoryCard.Matched();
            _secondMemoryCardTwo.Matched();
            UpdateResolvedPairs(+1);
        }

        ResetActualMemoryCardsOpen();
        yield return new WaitForSeconds(_intervalAllowFlip);
        CanFlipCardWasChanged?.Invoke(true);
    }

    private void ResetActualMemoryCardsOpen()
    {
        _firstMemoryCard = null;
        _secondMemoryCardTwo = null;
    }

    private void UpdateResolvedPairs(int newValue)
    {
        _resolvedPairs += newValue;

        if (_resolvedPairs >= _totalPairsNumber)
            FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        GameWasCompleted?.Invoke();
    }
}
