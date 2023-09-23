using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenLaser : Laser
{
    [SerializeField] private Laser _prefab;
    [SerializeField] private ParticleSystem _particle;

    protected override void OnEnemyCollision(Enemy enemy)
    {
        base.OnEnemyCollision(enemy);
        var hits = Physics2D.CircleCastAll(enemy.transform.position, 3, Vector2.zero);
        foreach (var hit in hits)
        {
            if(hit.collider.gameObject.TryGetComponent<Enemy>(out Enemy e))
                e.StartCoroutine(FrozenRoutine(e));
        }
    }

    private IEnumerator FrozenRoutine(Enemy enemy)
    {
        var sr = enemy.GetComponent<SpriteRenderer>();
        enemy.speedMultiplier *= 0.5f;
        var col = sr.color;
        col.r *= 0.6f;
        sr.color = col;
        Instantiate(_particle, enemy.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        enemy.speedMultiplier /= 0.5f;
        col = sr.color;
        col.r /= 0.6f;
        sr.color = col;
    }

    public override void Shoot(Shooter shooter)
    {
        base.Shoot(shooter);
        shooter.StartCoroutine(ShootRoutine(shooter));
    }

    private IEnumerator ShootRoutine(Shooter shooter)
    {
        var transform = shooter.transform;
        var angles = transform.rotation.eulerAngles;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.06f);
            float angle = Random.Range(-10, 10);
            Instantiate(_prefab, transform.position, Quaternion.Euler(angles + new Vector3(0, 0, angle)));
            SoundEffectManager.instance.Play("Laser", 0.07f, 0.6f);
        }
    }
}
