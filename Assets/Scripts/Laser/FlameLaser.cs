using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLaser : Laser
{
    [SerializeField] private ParticleSystem _particle;

    protected override void OnEnemyCollision(Enemy enemy)
    {
        base.OnEnemyCollision(enemy);
        var hits = Physics2D.CircleCastAll(enemy.transform.position, 5, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Enemy e))
                e.StartCoroutine(FlameRoutine(e));
        }
    }

    private IEnumerator FlameRoutine(Enemy enemy)
    {
        Instantiate(_particle, enemy.transform.position, Quaternion.identity);
        var sr = enemy.GetComponent<SpriteRenderer>();
        var col = sr.color;
        for (int i = 0; i < 6; i++)
        {
            col = sr.color;
            col.b *= 0.6f;
            col.g *= 0.6f;
            sr.color = col;
            enemy.hp -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }

        col = sr.color;
        for (int i = 0; i < 6; i++)
        {
            col.b /= 0.6f;
            col.g /= 0.6f;
        }

        sr.color = col;
    }
}
