using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhostAttack : MonoBehaviour
{
    public GameObject airTornadoParticles;
    public GameObject airTornadoDamageArea;
    public float airTornadoFollowRate = 10;
    private Vector2 airTornadoPos;

    private void Start()
    {
        airTornadoPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        airTornadoPos = Vector2.Lerp(transform.position, airTornadoPos,
            Mathf.Exp(-airTornadoFollowRate * Time.deltaTime));
        
        airTornadoParticles.transform.position = airTornadoPos;
        airTornadoDamageArea.transform.position = airTornadoPos;

        foreach (var particles in airTornadoParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = true;
        }

    }
    
}
