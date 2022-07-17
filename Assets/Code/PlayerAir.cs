using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    public GameObject airSlash;

    public GameObject airTornadoParticles;
    public GameObject airTornadoDamageArea;
    public float airTornadoFollowRate = 4;
    public float minAirTornadoTime = .2f;

    private float lastMouseDownTime;
    private Vector2 airTornadoPos;

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
            lastMouseDownTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time > lastMouseDownTime + minAirTornadoTime
                && Time.time - Time.deltaTime < lastMouseDownTime + minAirTornadoTime)
            {
                airTornadoPos = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time < lastMouseDownTime + minAirTornadoTime)
            {
                var newAirSlash = Instantiate(airSlash, Player.Instance.transform);
                newAirSlash.transform.position = transform.position;
                newAirSlash.transform.rotation = Quaternion.Euler(0, 0,
                    Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
                newAirSlash.transform.GetChild(0).localRotation = Quaternion.Euler(
                    Random.value > .5 ? 180 : 0, 0, 0);
                CameraFollow.Instance.Shake(.25f * (1 + LevelManager.Instance.upgradeAir2));

                newAirSlash.transform.localScale *= 1 + .5f * LevelManager.Instance.upgradeAir1;
                newAirSlash.GetComponentInChildren<DamageArea>().knockbackForce *=
                    1 + LevelManager.Instance.upgradeAir1;

                newAirSlash.GetComponentInChildren<DamageArea>().damagePerSecond *=
                    1 + LevelManager.Instance.upgradeAir2;
            }
        }

        var airTornadoActive = Input.GetMouseButton(0) && Time.time > lastMouseDownTime + minAirTornadoTime;
        var mousePos = (Vector2)transform.position + ToMouse();

        if (airTornadoActive)
        {
            airTornadoPos = Vector2.Lerp(mousePos, airTornadoPos,
                Mathf.Exp(-airTornadoFollowRate * Time.deltaTime));
        }

        airTornadoParticles.transform.position = airTornadoPos;
        airTornadoDamageArea.transform.position = airTornadoPos;

        airTornadoParticles.transform.localScale = Vector3.one * (1 + LevelManager.Instance.upgradeAir3);
        airTornadoDamageArea.GetComponent<DamageArea>().damagePerSecond = 1f * (1 + LevelManager.Instance.upgradeAir4);
        airTornadoDamageArea.GetComponent<DamageArea>().knockbackForce = -30 * (1 + LevelManager.Instance.upgradeAir4);

        airTornadoDamageArea.SetActive(airTornadoActive);
        foreach (var particles in airTornadoParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = airTornadoActive;

            if (particles.name == "EarthDustUpParticles")
            {
                emission.enabled = Input.GetMouseButton(0) && Time.time > lastMouseDownTime + minAirTornadoTime + .1f;
            }
        }
    }
}