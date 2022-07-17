using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    public GameObject airStreamParticles;
    public GameObject airStreamDamageArea;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private void Update()
    {
        var airStreamActive = Input.GetMouseButton(0);

        airStreamParticles.transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
        airStreamDamageArea.transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));

        airStreamDamageArea.SetActive(airStreamActive);
        foreach (var particles in airStreamParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = airStreamActive;
        }
    }
}