using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public float invincibilityTime;
    private float currentInvincibilityTime;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentInvincibilityTime > 0) return;

        currentHealth -= damage;
        currentInvincibilityTime = invincibilityTime;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        currentInvincibilityTime -= Time.deltaTime;
    }
}