using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    public GameObject waterStreamParticles;
    public GameObject waterStreamDamageArea;

    private Vector2 ToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var toMouse = mousePos - transform.position;
        return toMouse;
    }

    private void Update()
    {
        var waterStreamActive = Input.GetMouseButton(0);

        waterStreamParticles.transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));
        waterStreamDamageArea.transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Rad2Deg * Mathf.Atan2(ToMouse().y, ToMouse().x));

        waterStreamDamageArea.SetActive(waterStreamActive);
        foreach (var particles in waterStreamParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = waterStreamActive;
        }
    }
}