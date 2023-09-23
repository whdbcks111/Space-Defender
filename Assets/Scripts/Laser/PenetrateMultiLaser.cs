using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateMultiLaser : MultiLaser
{
    protected override void OnEnemyCollision(Enemy enemy)
    {
        Damage(enemy);
    }
}
