using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Pool;
public class CircleController : MoveController
{
    //public Transform bodyTank;
    //public Transform gun;
    //public Transform gun2; // súng đằng sau
    public float hp;

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
    }
    //protected void RotateGun(Vector3 direction)
    //{
    //    gun.up = direction;

    //    if (gun2 != null)
    //    {
    //        gun2.up = direction;
    //    }

    //}
    public virtual void CalculateHP(float value)
    {
        hp += value;
    }
}