using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    public float followRate = 2;
    public float unshakeRate = 2;
    public float shakeScale;

    private Vector2 currentPos;
    private float currentShake;
    private Vector3 offset;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private IEnumerator Start()
    {
        offset = transform.position;
        yield return new WaitForEndOfFrame();
        currentPos = Player.Instance.transform.position;
    }

    private void Update()
    {
        if (Player.Instance == null) return;

        Vector2 playerPos = Player.Instance.transform.position;
        currentPos = Vector2.Lerp(playerPos, currentPos, Mathf.Exp(-Time.deltaTime * followRate));

        currentShake *= Mathf.Exp(-Time.deltaTime * unshakeRate);

        var shake = currentShake * Random.insideUnitCircle;
        transform.position = (Vector3)(currentPos + shake) + offset;
    }

    public void Shake(float amount)
    {
        currentShake += shakeScale * amount;
    }
}