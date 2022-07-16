using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);

        var otherHealth = col.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }
    }
}