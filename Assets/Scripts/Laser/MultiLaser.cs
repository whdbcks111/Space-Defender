using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLaser : Laser
{
    public int count;
    public float angleSpan;

    public override void Shoot(Shooter shooter)
    {
        var transform = shooter.transform;
        var angles = transform.rotation.eulerAngles;
        for (int i = 0; i < count; i++)
        {
            float angle = -(count - 1) * angleSpan / 2f + i * angleSpan;
            Instantiate(this, transform.position, Quaternion.Euler(angles + new Vector3(0, 0, angle)));
        }
    }
}
