using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerEarth : MonoBehaviour
{
    public GameObject boulder;
    public float shootSpeed;

    public GameObject block;
    public float blockMinRange, blockMaxRange;
    public float minBlockTime = .2f;
    public GameObject blockTargetingParticles;

    private float lastMouseDownTime;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private Vector2 BlockTargetPos()
    {
        var toMouse = ToMouse();
        if (toMouse.magnitude < blockMinRange)
        {
            toMouse = toMouse.normalized * blockMinRange;
        }
        else if (toMouse.magnitude > blockMaxRange)
        {
            toMouse = toMouse.normalized * blockMaxRange;
        }

        return (Vector2)transform.position + toMouse;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseDownTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            blockTargetingParticles.transform.position = BlockTargetPos();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time < lastMouseDownTime + minBlockTime)
            {
                // Boulder
                var newBoulder = Instantiate(boulder);
                newBoulder.transform.position = transform.position;
                newBoulder.transform.rotation = quaternion.Euler(0, 0, Random.Range(0f, 360f));
                newBoulder.GetComponent<Rigidbody2D>().velocity = shootSpeed * ToMouse().normalized;
            }
            else
            {
                // Block
                var newBlock = Instantiate(block);
                newBlock.transform.position = BlockTargetPos();
            }
        }

        var targetingBlock = Input.GetMouseButton(0) && Time.time > lastMouseDownTime + minBlockTime;
        foreach (var particles in blockTargetingParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = targetingBlock;
        }
    }
}