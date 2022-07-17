using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public float minEnemyDist = 10;
    public float distToPlayer = 5;

    private void Update()
    {
        GameObject nearestEnemy = null;
        var distToNearestEnemy = 9999f;
        var camera = Camera.main.transform;

        foreach (var enemy in WorldGen.Instance.AllEnemies)
        {
            if (enemy != null)
            {
                var distToEnemy = ((Vector2)(enemy.transform.position - camera.position)).magnitude;
                if (distToEnemy < distToNearestEnemy)
                {
                    nearestEnemy = enemy;
                    distToNearestEnemy = distToEnemy;
                }
            }
        }

        if (distToNearestEnemy < minEnemyDist)
        {
            nearestEnemy = null;
        }

        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (nearestEnemy == null)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
            Vector2 toNearestEnemy = nearestEnemy.transform.position - camera.position;
            toNearestEnemy = toNearestEnemy.normalized * distToPlayer;

            transform.position = (Vector2)camera.position + toNearestEnemy;
            transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Rad2Deg * Mathf.Atan2(toNearestEnemy.y, toNearestEnemy.x));
        }
    }
}