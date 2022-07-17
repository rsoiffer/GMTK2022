using UnityEngine;

public class Flammable : MonoBehaviour
{
    public GameObject fireParticles;
    public GameObject fireSpreader;

    public float catchFireRate;
    public float maxFireDuration;
    public float fireDPS;

    private float fireTimer = -100;

    private void OnTriggerStay2D(Collider2D col)
    {
        var tags = col.gameObject.GetComponent<ElementTags>();
        if (tags == null) return;

        if (Random.value < Time.deltaTime * catchFireRate * tags.fire)
        {
            fireTimer = maxFireDuration;
        }

        if (fireTimer > 0)
        {
            if (tags.water || tags.earth)
            {
                fireTimer = 0;
            }
            else if (tags.air)
            {
                // spreadFireChance *= 2;
                var tags2 = fireSpreader.GetComponent<ElementTags>();
                tags2.fire *= 2;
            }
        }
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        var onFire = fireTimer > 0;

        if (onFire)
        {
            var myHealth = GetComponentInParent<Health>();
            if (myHealth != null)
            {
                myHealth.TakeDamage(fireDPS * Time.deltaTime);
            }
        }

        fireSpreader.SetActive(onFire);
        fireParticles.SetActive(fireTimer > -5);
        foreach (var particles in fireParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = onFire;
        }
    }
}