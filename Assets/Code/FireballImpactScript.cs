using UnityEngine;

public class FireballImpactScript : MonoBehaviour
{
    private void Start()
    {
        var damageArea = GetComponentInChildren<DamageArea>();
        damageArea.damagePerSecond *= 1 + LevelManager.Instance.upgradeFire2;
        damageArea.knockbackForce *= 1 + LevelManager.Instance.upgradeFire2;

        transform.localScale *= 1 + LevelManager.Instance.upgradeFire2;
    }
}