using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stashable : MonoBehaviour
{ 
    private const float payDuration = 0.3f;
    private const float completionRadius = .5f;
    private const float speed = 150f;
    public void CollectStashable(Transform stashParent, float yLocalPosition, Action onCompleteCollect)
    {
        
        Vector3 targetPos = stashParent.position + Vector3.up * yLocalPosition;
        Tweener tweener = transform.DOMove(targetPos, speed).SetSpeedBased(true);
        tweener.OnUpdate(delegate () {
            transform.LookAt(stashParent, Vector3.up);

            if (Vector3.Distance(transform.position, targetPos) > completionRadius)
            {
                targetPos = stashParent.position + Vector3.up * yLocalPosition; 
                tweener.ChangeEndValue(targetPos, true);
            }

        }).OnComplete(() => {
            transform.parent = stashParent;
            transform.localPosition = Vector3.up * yLocalPosition;
            transform.localRotation = Quaternion.identity;
            onCompleteCollect?.Invoke();
        });
    }
    public void PayStashable(Transform target, Action onCompletePay)
    {
        transform.parent = null;

        Vector3 targetPos = target.position;
        Vector3 direction = targetPos - transform.position;
        direction.y = 0;

        Vector3 middlePos = transform.position + direction / 2f;
        middlePos.y = transform.position.y + 2f;

        transform.DOPath(new [] { middlePos, targetPos }, payDuration, PathType.CatmullRom)
                    .OnComplete(() =>
                    {
                        onCompletePay?.Invoke();
                        Destroy(gameObject);
                    });
    }

}
