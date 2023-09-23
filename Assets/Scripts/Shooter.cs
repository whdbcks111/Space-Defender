using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    public Laser laserPrefab;

    private float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (HpManager.instance.hp <= 0) return;
        if (_timer > 0) _timer -= Time.deltaTime;

        if(Input.GetKey(KeyCode.Space) && _timer <= 0f)
        {
            _timer = laserPrefab.cooldown;
            laserPrefab.Shoot(this);
            SoundEffectManager.instance.Play("Laser", 0.1f, 3f);
        }
    }
}
