using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonEnemy : Enemy
{
    protected override void Update()
    {
        base.Update();
        transform.Rotate(0, 0, 360 * Time.deltaTime * 2);
        var scale = transform.localScale;
        scale += new Vector3(1, 1, 0) * Time.deltaTime * 0.3f;
    }
}
