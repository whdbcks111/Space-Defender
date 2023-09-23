using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    public float interval;
    
    private float _timer = 0;

    private void Update()
    {
        if (GameManager.instance.IsBreakTime) return;
        if ((_timer -= Time.deltaTime) < 0)
        {
            _timer += interval * Mathf.Pow(0.97f, GameManager.instance.phase);

            var spawnables = new List<Enemy>(_enemies).FindAll(enemy => (
                (enemy.minPhase <= GameManager.instance.phase || enemy.minPhase <= 0) &&
                (enemy.maxPhase >= GameManager.instance.phase || enemy.maxPhase <= 0)
                ));
            float totalWeight = 0;
            foreach(Enemy enemy in spawnables)
            {
                totalWeight += enemy.chanceWeight;
            }

            float randomValue = Random.value;
            float accWeight = 0;
            Enemy prefab = null;
            foreach(Enemy enemy in spawnables)
            {
                if(accWeight <= randomValue && randomValue < accWeight + enemy.chanceWeight / totalWeight)
                {
                    prefab = enemy;
                    break;
                }
                accWeight += enemy.chanceWeight / totalWeight;
            }

            if(prefab)
            {
                Enemy e = Instantiate(
                    prefab,
                    transform
                    );

                var leftTop = Camera.main.ViewportToWorldPoint(new(0, 1));
                var rightDown = Camera.main.ViewportToWorldPoint(new(1, 0));

                e.transform.position = new Vector2(Random.Range(leftTop.x + 1, rightDown.x - 1), leftTop.y + 1);
            }
        }
    }
}
