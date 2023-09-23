using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticle : MonoBehaviour
{
    private ParticleSystem _self;

    private void Awake()
    {
        _self = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(!_self.IsAlive()) Destroy(gameObject);
    }
}
