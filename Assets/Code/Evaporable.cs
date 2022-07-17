using UnityEngine;

public class Evaporable : MonoBehaviour
{
    public float evaporationTime = 1;

    private void OnTriggerStay2D(Collider2D col)
    {
        var tags = col.gameObject.GetComponent<ElementTags>();
        if (tags == null) return;

        var evaporationPower = tags.air ? 1 : 0 + tags.fire;
        evaporationTime -= Time.deltaTime * evaporationPower;
        if (evaporationTime < 0)
        {
            Destroy(gameObject);
        }
    }
}