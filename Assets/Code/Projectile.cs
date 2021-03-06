using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1;

    public GameObject detachOnDeath;
    public float detachLifetime;

    public GameObject spawnOnDeath;

    public float shakeOnHit;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        CameraFollow.Instance.Shake(shakeOnHit);

        if (detachOnDeath != null)
        {
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
        }

        if (spawnOnDeath != null)
        {
            var newSpawn = Instantiate(spawnOnDeath);
            newSpawn.transform.position = transform.position;
        }

        var otherHealth = col.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }
    }
}