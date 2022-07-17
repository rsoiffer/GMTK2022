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
    public float blockShake = .5f;
    public GameObject blockTargetingParticles;
    public GameObject blockTargetDamageArea;

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
            blockTargetDamageArea.transform.position = BlockTargetPos();

            blockTargetDamageArea.GetComponentInChildren<DamageArea>().damagePerSecond =
                0.5f * (1 + LevelManager.Instance.upgradeEarth4);
            blockTargetDamageArea.GetComponentInChildren<DamageArea>().knockbackForce =
                5 * (1 + LevelManager.Instance.upgradeEarth4);
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

                newBoulder.GetComponent<Projectile>().damage *= 1 + LevelManager.Instance.upgradeEarth1;
                newBoulder.GetComponent<Projectile>().shakeOnHit *= 1 + LevelManager.Instance.upgradeEarth1;
                newBoulder.GetComponent<Rigidbody2D>().mass *= 1 + LevelManager.Instance.upgradeEarth1;

                newBoulder.GetComponent<Rigidbody2D>().velocity *= 1 + LevelManager.Instance.upgradeEarth2;
                newBoulder.GetComponent<Projectile>().shakeOnHit *= 1 + LevelManager.Instance.upgradeEarth2;
            }
            else
            {
                // Block
                var newBlock = Instantiate(block);
                newBlock.transform.position = BlockTargetPos();
                CameraFollow.Instance.Shake(blockShake * (1 + LevelManager.Instance.upgradeEarth4));

                newBlock.transform.localScale *= 1 + LevelManager.Instance.upgradeEarth3;
                var newBlockHealth = newBlock.GetComponent<Health>();
                newBlockHealth.TakeDamage(-newBlockHealth.maxHealth * LevelManager.Instance.upgradeEarth3);

                newBlock.GetComponentInChildren<DamageArea>().damagePerSecond *=
                    1 + LevelManager.Instance.upgradeEarth4;
                newBlock.GetComponentInChildren<DamageArea>().knockbackForce *=
                    1 + LevelManager.Instance.upgradeEarth4;
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