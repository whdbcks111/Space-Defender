using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevineLaser : MultiLaser
{
    [SerializeField] private Laser _prefab;

    protected override void OnEnemyCollision(Enemy enemy)
    {
        Instantiate(_prefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        base.OnEnemyCollision(enemy);
    }
}
