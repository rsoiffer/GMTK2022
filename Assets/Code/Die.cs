using System.Collections;
using UnityEngine;

public class Die : MonoBehaviour
{
    public Door door;
    public GameObject upgrade;
    public Sprite dieSprite;
    public float shake = 1;
    
    private bool unlocked;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        while (!WorldGen.Instance.AllEnemiesDead)
        {
            yield return new WaitForEndOfFrame();
        }

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dieSprite;
        unlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.activeInHierarchy && unlocked && col.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.Reroll();
            gameObject.SetActive(false);
            upgrade.SetActive(true);
            door.Unlock();
            CameraFollow.Instance.Shake(shake);
        }
    }
}