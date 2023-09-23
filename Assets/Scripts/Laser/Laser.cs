using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float damage;
    public int price;
    public Laser[] upgrades;
    public float cooldown;
    private float _dur = 10f;

    void Update()
    {
        var angleZ = transform.rotation.eulerAngles.z + 90;
        var dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleZ), Mathf.Sin(Mathf.Deg2Rad * angleZ));
        transform.position += speed * Time.deltaTime * dir;

        speed += acceleration * Time.deltaTime;

        if ((_dur -= Time.deltaTime) < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            OnEnemyCollision(enemy);
        }
    }

    public virtual void Shoot(Shooter shooter)
    {
        var t = shooter.transform;
        Instantiate(this, t.position, t.rotation);
    }

    protected virtual void Damage(Enemy enemy)
    {
        enemy.hp -= damage;
    }

    protected virtual void OnEnemyCollision(Enemy enemy)
    {
        Damage(enemy);
        Destroy(gameObject);
    }
}
