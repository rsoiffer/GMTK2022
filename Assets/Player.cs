using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform camera;
    public float mouseSensitivity;

    public float moveSpeed;
    public float accelRate;

    private Rigidbody myRigidbody;
    private float yaw, pitch;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var h = mouseSensitivity * Input.GetAxis("Mouse X");
        var v = mouseSensitivity * Input.GetAxis("Mouse Y");
        yaw += h * Time.deltaTime;
        pitch -= v * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -80, 80);
        camera.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void FixedUpdate()
    {
        myRigidbody.rotation = Quaternion.Euler(0, yaw, 0);

        var goalVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            goalVelocity += myRigidbody.transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            goalVelocity -= myRigidbody.transform.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            goalVelocity -= myRigidbody.transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            goalVelocity += myRigidbody.transform.right;
        }

        if (goalVelocity.magnitude > 1e-3)
        {
            goalVelocity = goalVelocity.normalized * moveSpeed;
        }

        var toGoalVelocity = goalVelocity - myRigidbody.velocity;
        toGoalVelocity.y = 0;
        myRigidbody.AddForce(toGoalVelocity * accelRate, ForceMode.Acceleration);
    }
}