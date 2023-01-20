using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform TargetTransform;
    public Vector3 offset;
    public float cameraSpeed;

    private void Update()
    {
        Vector3 newPosition = TargetTransform.position + offset;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * cameraSpeed);
    }
}
