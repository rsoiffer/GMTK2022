using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float damage = 1f;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var otherHealth = other.gameObject.GetComponent<Health>();
            if (otherHealth != null)
            {
                otherHealth.TakeDamage(damage);
            }
        }
    }
}