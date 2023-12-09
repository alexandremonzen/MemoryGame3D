using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public sealed class MemoryCardsPlacement : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private int _cardAmount = 12;
    private List<MemoryCard> _cards;

    [Header("Distribuiton settings")]
    [SerializeField] private bool _centerCards = true;
    [SerializeField] private int _cardsPerRow = 4;
    [SerializeField] private float _xDistBetweenObjects = 0.2f;
    [SerializeField] private float _zDistBetweenObjects = 0.25f;

    private float _xTotalDistance;
    private float _zTotalDistance;
    private int _actualCardsPerRow;

    private float _xHighestValue;
    private float _zHighestValue;

    public void CreateMemoryCardsInEditor()
    {
#if UNITY_EDITOR
        ResetAllValues();
        CheckPairsNumber();

        _cards = new List<MemoryCard>();
        GetComponentsInChildren(_cards);

        DestroyExistingCards();
        _cards.Clear();

        for (int i = 0; i < _cardAmount; i++)
        {
            Object obj = PrefabUtility.InstantiatePrefab(_cardPrefab);
            GameObject gameObject = obj as GameObject;
            MemoryCard memoryCard = SetupMemoryCard(gameObject, i);

            _xTotalDistance += _xDistBetweenObjects;
            _cards.Add(memoryCard);
            _actualCardsPerRow++;

            UpdateHighestValues(memoryCard.transform);
            CheckCardsInRow();
        }

        CenterPositionGameObjects();
#endif
    }

    public List<MemoryCard> CreateCardsInGameplay()
    {
        _cardAmount = PlayerPrefs.GetInt("numberCardPairs", 6) * 2;
        SetCardsPerRowAutomatic();

        ResetAllValues();

        _cards = new List<MemoryCard>();
        GetComponentsInChildren(_cards);

        DestroyExistingCards();
        _cards.Clear();

        for (int i = 0; i < _cardAmount; i++)
        {
            GameObject gameObject = Instantiate(_cardPrefab, this.transform);
            MemoryCard memoryCard = SetupMemoryCard(gameObject, i);

            _xTotalDistance += _xDistBetweenObjects;
            _cards.Add(memoryCard);
            _actualCardsPerRow++;

            UpdateHighestValues(memoryCard.transform);
            CheckCardsInRow();
        }

        CenterPositionGameObjects();

        return _cards;
    }

    private MemoryCard SetupMemoryCard(GameObject gameObject, int index)
    {
        MemoryCard memoryCard = gameObject.GetComponent<MemoryCard>();
        memoryCard.transform.position = new Vector3(_xTotalDistance, this.transform.position.y, _zTotalDistance);
        memoryCard.transform.parent = this.transform;
        memoryCard.name = $"{gameObject.name} ({index})";

        return memoryCard;
    }

    private void CheckPairsNumber()
    {
        if (_cardAmount % 2 != 0)
        {
            _cardAmount += 1;
            Debug.LogWarning("The card amount it's not a pair number, rounding up");
        }
    }

    private void ResetAllValues()
    {
        _xTotalDistance = 0;
        _zTotalDistance = 0;
        _actualCardsPerRow = 0;

        _xHighestValue = 0;
        _zHighestValue = 0;
    }

    private void SetCardsPerRowAutomatic()
    {
        _cardsPerRow = 4;

        if (_cardAmount >= 36)
        {
            _cardsPerRow = 10;
            return;
        }

        if (_cardAmount >= 18)
            _cardsPerRow = 7;
    }

    private void DestroyExistingCards()
    {
        if (_cards.Count <= 0)
            return;

        foreach (MemoryCard memoryCard in _cards)
            DestroyImmediate(memoryCard.gameObject);
    }

    private void UpdateHighestValues(Transform transformObj)
    {
        if (transformObj.position.x > _xHighestValue)
            _xHighestValue = transformObj.position.x;

        if (transformObj.position.z < _zHighestValue)
            _zHighestValue = transformObj.position.z;
    }

    private void CheckCardsInRow()
    {
        if (_actualCardsPerRow >= _cardsPerRow)
        {
            _actualCardsPerRow = 0;
            _xTotalDistance = 0;
            _zTotalDistance -= _zDistBetweenObjects;
        }
    }

    private void CenterPositionGameObjects()
    {
        if (!_centerCards)
            return;

        float xMagicNumber = _xHighestValue / 2;
        float zMagicNumber = _zHighestValue / 2;

        foreach (MemoryCard memoryCard in _cards)
            memoryCard.transform.position -= new Vector3(xMagicNumber, 0, zMagicNumber);
    }
}