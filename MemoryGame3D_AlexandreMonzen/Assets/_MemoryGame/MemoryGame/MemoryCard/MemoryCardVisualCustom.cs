using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MemoryCardVisualCustom : MonoBehaviour
{
    private MemoryCard _memoryCard;
    private SetTextureArray2D _iconTexture;

    private void Awake()
    {
        _memoryCard = GetComponentInParent<MemoryCard>();
        _iconTexture = GetComponentInChildren<SetTextureArray2D>();
    }

    private void OnEnable()
    {
        _memoryCard.VisualChangeWasRequested += SetVisual;
    }

    private void OnDisable()
    {
        _memoryCard.VisualChangeWasRequested -= SetVisual;
    }

    private void SetVisual(int index)
    {
        _iconTexture.SetSliceFromTextureArray(index);
    }
}
