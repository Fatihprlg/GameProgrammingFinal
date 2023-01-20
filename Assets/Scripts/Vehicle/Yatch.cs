using DG.Tweening;
using UnityEngine;

public class Yatch : Vehicle
{
    [SerializeField] private StashDischarger discharger;
    [SerializeField] private Vector3 cameraOffset;
    
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startCameraOffset;
    
    private void Start()
    {
        startPos = movement.transform.position;
        startRot = movement.transform.rotation;
        movement.enabled = false;
        collector.enabled = false;
    }

    protected override void OnPlayerEntered()
    {
        base.OnPlayerEntered();
        startCameraOffset = cameraController.offset;
        cameraController.offset = cameraOffset;
    }

    protected override void Leave()
    {
        
        movement.transform.DOMove(startPos, 1f).SetId(this).OnComplete(()=>
        {
            discharger.Discharge();
            cameraController.offset = startCameraOffset;
            base.Leave();
        });
        movement.transform.DORotateQuaternion(startRot, .7f);
    }
}
