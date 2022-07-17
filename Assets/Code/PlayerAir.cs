using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    public GameObject airTornadoParticles;
    public GameObject airTornadoDamageArea;

    public float airTornadoFollowRate = 4;
    private Vector2 airTornadoPos;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private void Update()
    {
        var airStreamActive = Input.GetMouseButton(0);
        var mousePos = (Vector2)transform.position + ToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            airTornadoPos = transform.position;
        }

        if (airStreamActive)
        {
            airTornadoPos = Vector2.Lerp(mousePos, airTornadoPos,
                Mathf.Exp(-airTornadoFollowRate * Time.deltaTime));
        }

        airTornadoParticles.transform.position = airTornadoPos;
        airTornadoDamageArea.transform.position = airTornadoPos;

        airTornadoDamageArea.SetActive(airStreamActive);
        foreach (var particles in airTornadoParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = airStreamActive;
        }
    }
}