using UnityEngine;

using Monzen.Modules.Audio;

public sealed class MemoryCardAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _flipAudio;
    [SerializeField] private AudioClip _unflipAudio;
    [SerializeField] private AudioClip _matchedAudio;

    private MemoryCard _memoryCard;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _memoryCard = GetComponent<MemoryCard>();
    }

    private void OnEnable()
    {
        _memoryCard.CardWasFlipped += () => PlayAudio(_flipAudio, 0.5f, 0.6f);
        _memoryCard.CardWasUnFlipped += () => PlayAudio(_unflipAudio, 0.5f, 0.6f);
        _memoryCard.CardWasMatched += () => PlayAudio(_matchedAudio, 0, 0.6f);
    }

    private void OnDisable()
    {
        _memoryCard.CardWasFlipped -= () => PlayAudio(_flipAudio, 0.5f, 0.6f);
        _memoryCard.CardWasUnFlipped -= () => PlayAudio(_unflipAudio, 0.5f, 0.6f);
        _memoryCard.CardWasMatched -= () => PlayAudio(_matchedAudio, 0, 0.6f);
    }

    private void PlayAudio(AudioClip audioClip, float pitchRange = 0, float volume = 1)
    {
        _audioManager.PlayAudio3D(audioClip, this.transform.position, pitchRange, volume);
    }
}
