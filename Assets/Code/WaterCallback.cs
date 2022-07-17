using System.Collections.Generic;
using UnityEngine;

public class WaterCallback : MonoBehaviour
{
    public float damage;
    public float shakeOnHit;
    public float impulseOnHit;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new();

    private void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        var numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        CameraFollow.Instance.Shake(shakeOnHit * numCollisionEvents);

        var otherHealth = other.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage * numCollisionEvents);
        }

        var otherRigidbody = other.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            var toOther = otherRigidbody.worldCenterOfMass - (Vector2)transform.position;
            otherRigidbody.AddForce(impulseOnHit * toOther.normalized, ForceMode2D.Impulse);
        }
    }
}