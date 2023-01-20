using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Movement
{
    [SerializeField] private WheelRotation wheelRot;
    [SerializeField] private float wheelSpeed;

    protected override void MovementUpdate()
    {
        base.MovementUpdate();
        Vector3 movementVector = new (joystick.Direction.x, 0, joystick.Direction.y);
        wheelRot.SetSpeed((movementVector * wheelSpeed).magnitude);
    }
}