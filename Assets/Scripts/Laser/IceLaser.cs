using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaser : Laser
{

    protected override void OnEnemyCollision(Enemy enemy)
    {
        base.OnEnemyCollision(enemy);
        if(enemy.speedMultiplier > 0.5f) enemy.speedMultiplier *= 0.9f;
    }
}
