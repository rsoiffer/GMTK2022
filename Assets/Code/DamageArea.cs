using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float damagePerSecond;
    public float knockbackForce;

    private void OnTriggerStay2D(Collider2D other)
    {
        var otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }

        var otherRigidbody = other.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            var toOther = otherRigidbody.worldCenterOfMass - (Vector2)transform.position;
            otherRigidbody.AddForce(knockbackForce * toOther.normalized);
        }
    }
}