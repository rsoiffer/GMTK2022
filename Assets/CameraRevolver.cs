using UnityEngine;

public class CameraRevolver : MonoBehaviour
{
    [SerializeField] private float radius;
    private new Transform transform;
    private Vector3 center;
    private float speed = 0.1f;

    private void Start()
    {
        transform = GetComponent<Transform>();
        center = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(
            center.x + radius * Mathf.Sin(speed * Time.time),
            center.y + radius * Mathf.Cos(speed * Time.time),
            transform.position.z);
    }
}