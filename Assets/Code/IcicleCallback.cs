using System.Collections.Generic;
using UnityEngine;

public class IcicleCallback : MonoBehaviour
{
    public float minAngle, maxAngle;

    public float damage;
    public float shakeOnHit;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new();

    private void Start()
    {
        part = GetComponent<ParticleSystem>();
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle));
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collision");

        var numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        CameraFollow.Instance.Shake(shakeOnHit * numCollisionEvents);

        var otherHealth = other.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage * numCollisionEvents);
        }
    }
}