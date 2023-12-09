using System;
using System.Collections;
using UnityEngine;

public class CommonUI : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCanvasGroupValues(float alpha, bool interactable, bool blockRayCasts, float delay = 0)
    {
        if(delay > 0)
        {
            StartCoroutine(DelaySetCanvas(alpha, interactable, blockRayCasts, delay));
            return;
        }

        _canvasGroup.alpha = alpha;
        _canvasGroup.interactable = interactable;
        _canvasGroup.blocksRaycasts = blockRayCasts;
    }

    private IEnumerator DelaySetCanvas(float alpha, bool interactable, bool blockRayCasts, float time)
    {
        yield return new WaitForSeconds(time);
        SetCanvasGroupValues(alpha, interactable, blockRayCasts);
    }
}
