using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCardVFX : MonoBehaviour
{
    private List<ParticleSystem> _particleSystems;
    private MemoryCard _memoryCard;

    private void Awake()
    {
        _memoryCard = GetComponentInParent<MemoryCard>();
        _particleSystems = new List<ParticleSystem>();
        GetComponentsInChildren(_particleSystems);
    }

    private void OnEnable()
    {
        _memoryCard.CardWasMatched += PlayVFX;
    }

    private void OnDisable()
    {
        _memoryCard.CardWasMatched -= PlayVFX;
    }

    private void PlayVFX()
    {
        foreach(ParticleSystem particle in _particleSystems)
        {
            particle.Play();
        }
    }
}
