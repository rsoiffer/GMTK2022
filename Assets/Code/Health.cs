using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float CurrentHealth { get; private set; }

    public float invincibilityTime;
    private float currentInvincibilityTime;

    public float shakeOnDamage;
    public AudioClip damageSound;

    private void Awake() => CurrentHealth = maxHealth;

    public void TakeDamage(float damage)
    {
        if (currentInvincibilityTime > 0) return;

        CurrentHealth -= damage;
        currentInvincibilityTime = invincibilityTime;
        CameraFollow.Instance.Shake(shakeOnDamage);
        
        AudioSource audio = GetComponent<AudioSource>();
        if (damageSound && audio)
        {
            audio.clip = damageSound;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        currentInvincibilityTime -= Time.deltaTime;
    }
}