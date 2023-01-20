using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected VariableJoystick joystick;
    [SerializeField] protected Animator animCtrl;
    [SerializeField] protected float Speed = 5f;
    [SerializeField] protected float RotationSpeed = 10f;
    [SerializeField] private Vector2 movementMaxLimits;
    [SerializeField] private Vector2 movementMinLimits;
    
    private void Update()
    {
        MovementUpdate();
    }

    protected virtual void MovementUpdate()
    {
        if (joystick == null)
            return;

        Vector2 direction = joystick.Direction;

        Vector3 movementVector = new (direction.x, 0, direction.y);

        Vector3 currentPos = transform.position;
        Vector3 nextPos = currentPos + movementVector;
        var clampedX = Mathf.Clamp(nextPos.x, movementMinLimits.x, movementMaxLimits.x);
        var clampedZ = Mathf.Clamp(nextPos.z, movementMinLimits.y, movementMaxLimits.y);
        nextPos = new Vector3(clampedX, transform.position.y, clampedZ);
        transform.position = Vector3.Lerp(currentPos, nextPos, Time.deltaTime * Speed);

        if (movementVector.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementVector, Vector3.up), Time.deltaTime * RotationSpeed);
        }

        bool isWalking = direction.magnitude > 0;
        if (animCtrl == null)
            return;
        animCtrl.SetBool("IsWalking", isWalking);
        animCtrl.SetFloat("SpeedValue", direction.magnitude);
    }
}
