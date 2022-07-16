using UnityEngine;

public class Flammable : MonoBehaviour
{
    public GameObject fireParticles;
    public GameObject fireSpreader;

    public float catchFireRate;
    public float spreadFireChance;
    public float maxFireDuration;
    public float fireDPS;

    private float fireTimer = -100;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fire Element"))
        {
            if (Random.value < Time.deltaTime * catchFireRate)
            {
                fireTimer = maxFireDuration;
            }
        }
        else if (fireTimer > 0)
        {
            if (col.gameObject.CompareTag("Water Element") || col.gameObject.CompareTag("Earth Element"))
            {
                fireTimer = 0;
            }
            else if (col.gameObject.CompareTag("Air Element"))
            {
                spreadFireChance *= 2;
            }
        }
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        var onFire = fireTimer > 0;

        if (onFire)
        {
            var myHealth = GetComponent<Health>();
            if (myHealth != null)
            {
                myHealth.TakeDamage(fireDPS * Time.deltaTime);
            }
        }

        fireSpreader.SetActive(onFire && Random.value < spreadFireChance);
        fireParticles.SetActive(fireTimer > -5);
        foreach (var particles in fireParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = onFire;
        }
    }
}