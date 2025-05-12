using UnityEngine;
using DG.Tweening;
using System.Collections;

public static class DotweenUtils
{
    public static void PlayEnterFromOffset(
        MonoBehaviour mono,
        Transform target,
        Vector3 offset,
        float delay = 0f,
        float duration = 1f,
        Ease ease = Ease.OutBack)
    {
        mono.StartCoroutine(EnterFromOffsetRoutine(target, offset, delay, duration, ease));
    }

    private static IEnumerator EnterFromOffsetRoutine(
        Transform target,
        Vector3 offset,
        float delay,
        float duration,
        Ease ease)
    {
        Vector3 originalPos = target.position;
        target.position = originalPos + offset;

        if (delay > 0)
            yield return new WaitForSeconds(delay);

        target.DOMove(originalPos, duration).SetEase(ease);
    }
}
