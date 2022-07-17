﻿using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject fireball;
    public float shootSpeed;

    public GameObject fireStreamParticles;
    public GameObject fireStreamDamageArea;
    public float minStreamTime = .2f;

    private float lastMouseDownTime;
    private bool fireStreamActive;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private void Update()
    {
        fireStreamActive = false;

        if (Input.GetMouseButtonDown(0))
        {
            lastMouseDownTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time > lastMouseDownTime + minStreamTime)
            {
                // Fire stream
                fireStreamActive = true;
                fireStreamParticles.transform.rotation = Quaternion.Euler(0, 0,
                    Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
                fireStreamDamageArea.transform.rotation = Quaternion.Euler(0, 0,
                    Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time < lastMouseDownTime + minStreamTime)
            {
                // Fireball
                var newFireball = Instantiate(fireball);
                newFireball.transform.position = transform.position;
                newFireball.GetComponent<Rigidbody2D>().velocity = shootSpeed * ToMouse().normalized;
            }
        }

        fireStreamDamageArea.SetActive(fireStreamActive);
        foreach (var particles in fireStreamParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = fireStreamActive;
        }
    }
}