using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1;

    public GameObject detachOnDeath;
    public float detachLifetime;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        detachOnDeath.transform.parent = null;
        if (detachLifetime != 0)
        {
            Destroy(detachOnDeath, detachLifetime);
        }

        foreach (var particles in detachOnDeath.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = false;
        }

        var otherHealth = col.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }
    }
}