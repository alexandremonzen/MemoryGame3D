using System;
using UnityEngine;

public sealed class MemoryCard : MonoBehaviour, IInteractableCursor
{
    private int _cardValue;
    private PuzzleMemoryGame _puzzleMemoryCard;

    private bool _canBeFlipped;
    private bool _alreadyMatched;

    public event Action<int> VisualChangeWasRequested;
    public event Action CardWasFlipped;
    public event Action CardWasUnFlipped;
    public event Action CardWasMatched;

    #region Getters & Setters
    public int CardValue { get => _cardValue; }
    public bool AlreadyMatched { get => _alreadyMatched; set => _alreadyMatched = value; }
    public PuzzleMemoryGame PuzzleMemoryCard { get => _puzzleMemoryCard; set => _puzzleMemoryCard = value; }
    #endregion

    private void Awake()
    {
        _cardValue = -1;
        _puzzleMemoryCard = GetComponentInParent<PuzzleMemoryGame>();
        
        _canBeFlipped = true;
    }

    private void OnEnable()
    {
        _puzzleMemoryCard.CanFlipCardWasChanged += SetCanBeFlipped;
    }

    private void OnDisable()
    {
        _puzzleMemoryCard.CanFlipCardWasChanged -= SetCanBeFlipped;
    }

    public void InteractCursor()
    {
        if (!_canBeFlipped)
            return;

        if (!_alreadyMatched)
        {
            FlipThisCard();
        }
    }

    private void FlipThisCard()
    {
        _canBeFlipped = false;

        _puzzleMemoryCard.UpdateActualCards(this);
        CardWasFlipped?.Invoke();
    }

    public void UnFlipCard()
    {
        _canBeFlipped = true;
        CardWasUnFlipped?.Invoke();
    }

    public void Matched()
    {
        _alreadyMatched = true;
        CardWasMatched?.Invoke();
    }

    public void SetValueCard(int newValue)
    {
        _cardValue = newValue;
        VisualChangeWasRequested?.Invoke(_cardValue);
    }

    public void SetCanBeFlipped(bool condition)
    {
        _canBeFlipped = condition;
    }
}
