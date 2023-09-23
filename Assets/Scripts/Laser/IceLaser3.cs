using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaser3 : Laser
{

    protected override void OnEnemyCollision(Enemy enemy)
    {
        base.OnEnemyCollision(enemy);
        if(enemy.speedMultiplier > 0.3f) enemy.speedMultiplier *= 0.85f;
    }

    public override void Shoot(Shooter shooter)
    {
        shooter.StartCoroutine(ShootRoutine(shooter));
    }

    private IEnumerator ShootRoutine(Shooter shooter)
    {
        var transform = shooter.transform;
        var angles = transform.rotation.eulerAngles;
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.06f);
            float angle = Random.Range(-10, 10);
            Instantiate(this, transform.position, Quaternion.Euler(angles + new Vector3(0, 0, angle)));
            SoundEffectManager.instance.Play("Laser", 0.06f, 5f);
        }
    }
}
