using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}