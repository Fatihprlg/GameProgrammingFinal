using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    public int CollectedCount => CollectedObjects.Count;
    public Vector3 StashParentPos => stashParent.transform.position;
    [SerializeField] private Transform stashParent;
    [SerializeField] private List<Stashable> CollectedObjects;
    [SerializeField] private float collectionHeight = 1;
    [SerializeField] private int maxCollectableCount = 5;

    public void AddStash(Collectable collectedObject)
    {
        if (CollectedCount >= maxCollectableCount)
            return;

        var yLocalPosition = CollectedCount * collectionHeight;

        Stashable stashable = collectedObject.Collect(); 
        stashable.CollectStashable(stashParent, yLocalPosition, null);
        CollectedObjects.Add(stashable);
         
    }

    public Stashable RemoveStash()
    {
        if (CollectedCount <= 0)
            return null;

        Stashable stashable = CollectedObjects[CollectedCount - 1];

        CollectedObjects.Remove(stashable);
        stashable.transform.parent = null;

        return stashable;

    }


}
