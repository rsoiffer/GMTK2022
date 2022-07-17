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
            airTornadoPos = transform.position;
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
                    Random.value > .5 ? 180 : 0, 0, -45);
                CameraFollow.Instance.Shake(.25f);
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

        airTornadoDamageArea.SetActive(airTornadoActive);
        foreach (var particles in airTornadoParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = airTornadoActive;
        }
    }
}