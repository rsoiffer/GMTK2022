using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float damagePerSecond;

    private void OnTriggerStay2D(Collider2D other)
    {
        var otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}