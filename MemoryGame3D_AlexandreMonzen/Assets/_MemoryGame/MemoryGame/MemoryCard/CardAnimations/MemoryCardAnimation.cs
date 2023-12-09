using UnityEngine;

public class MemoryCardAnimation : MonoBehaviour
{
    private Animator _animator;
    private MemoryCard _memoryCard;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _memoryCard = GetComponentInParent<MemoryCard>();
    }

    private void OnEnable()
    {
        _memoryCard.CardWasFlipped += () => SetTriggerAnimation("cardFlip");
        _memoryCard.CardWasUnFlipped += () => SetTriggerAnimation("cardUnflip");
    }

    private void OnDisable()
    {
        _memoryCard.CardWasFlipped -= () => SetTriggerAnimation("cardFlip");
        _memoryCard.CardWasUnFlipped -= () => SetTriggerAnimation("cardUnflip");
    }

    private void SetTriggerAnimation(string animParameter)
    {
        _animator.SetTrigger(animParameter);
    }
}
