using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private bool isPlayer = false;
    public float currentHealth;
    private GameObject player;
    private void Awake()
    {
        isPlayer = gameObject.CompareTag("Player");
        if (isPlayer)
        {
            player = gameObject;
        }
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (isPlayer)
        {
            Player.Instance.InvFrames();
        }
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
    
    public void setHealth(float hp)
    {
        currentHealth = hp;
    }
}