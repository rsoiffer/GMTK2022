using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    public GameObject icicle;

    public GameObject waterStreamParticles;
    public GameObject waterStreamDamageArea;
    public GameObject waterPuddle;
    public float puddleSpawnInterval = 1f;
    public float puddleSpawnDistance = 2f;
    public float minStreamTime = .2f;

    private float lastPuddleTime;
    private float lastMouseDownTime;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPuddleTime = Time.time;
            lastMouseDownTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            waterStreamParticles.transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
            waterStreamDamageArea.transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));

            var waterCallback = waterStreamParticles.GetComponentInChildren<WaterCallback>();
            waterCallback.damage = .01f * (1 + LevelManager.Instance.upgradeWater3);
            var main = waterCallback.GetComponent<ParticleSystem>().main;
            main.startSpeedMultiplier = 20 * (1 + LevelManager.Instance.upgradeWater3);

            waterCallback.shakeOnHit = .01f * (1 + LevelManager.Instance.upgradeWater4);
            waterCallback.impulseOnHit = .5f * (1 + LevelManager.Instance.upgradeWater4);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time < lastMouseDownTime + minStreamTime)
            {
                // Icicle
                for (int i = 0; i < 1 + LevelManager.Instance.upgradeWater1; i++)
                {
                    var newIcicle = Instantiate(icicle);
                    newIcicle.transform.position = transform.position;
                    newIcicle.transform.rotation = Quaternion.Euler(0, 0,
                        Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
                    newIcicle.transform.localScale *= 1 + LevelManager.Instance.upgradeWater2;
                }
            }
        }

        var waterStreamActive = Input.GetMouseButton(0) && Time.time > lastMouseDownTime + minStreamTime;

        waterStreamDamageArea.SetActive(waterStreamActive);
        foreach (var particles in waterStreamParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = waterStreamActive;
        }

        if (waterStreamActive && Time.time > lastPuddleTime + puddleSpawnInterval)
        {
            lastPuddleTime = Time.time;
            var newPuddle = Instantiate(waterPuddle);
            newPuddle.transform.position = transform.position + (Vector3)ToMouse().normalized * puddleSpawnDistance;
        }
    }
}