using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int minPhase, maxPhase;
    public float chanceWeight;
    [SerializeField] protected float _damage;
    [SerializeField] protected Vector2 _velocity, _acceleration;

    [HideInInspector] public float speedMultiplier = 1f;

    [SerializeField] private float _maxHp;
    [HideInInspector] public float hp;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        hp = _maxHp;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        _velocity += _acceleration * Time.deltaTime;
        transform.Translate(_velocity * Time.deltaTime * speedMultiplier, Space.World);

        if(hp <= 0)
        {
            GameManager.instance.gold += Random.Range(3, 15) * 5;
            Explode();
        }

        var col = _spriteRenderer.color;
        col.a = (hp / _maxHp) * 0.7f + 0.3f;
        _spriteRenderer.color = col;
    }

    public void Explode()
    {
        SoundEffectManager.instance.Play("Boom", 0.1f, 0.7f);
        Instantiate(GameManager.instance.boomParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            HpManager.instance.hp -= _damage;
            Explode();
        }
    }
}
