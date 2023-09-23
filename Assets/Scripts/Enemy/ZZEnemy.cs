using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZEnemy : Enemy
{

    private float _timer = 0f;
    [SerializeField] private float _changeXInterval;

    protected override void Update()
    {
        base.Update();
        if((_timer -= Time.deltaTime) <= 0)
        {
            _timer = _changeXInterval;
            _velocity.x *= -1;
        }
    }
}
