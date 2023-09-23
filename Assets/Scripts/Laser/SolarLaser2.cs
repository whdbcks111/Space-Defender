using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarLaser2 : Laser
{
    [SerializeField] private Laser _prefab;
    protected override void OnEnemyCollision(Enemy enemy)
    {
        Damage(enemy);
        if (enemy.hp <= 0)
        {
            for (float i = 0f; i < 360f; i += 60f)
            {
                var clone = Instantiate(_prefab, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + i));
            }
        }

        Destroy(gameObject);
    }
}
