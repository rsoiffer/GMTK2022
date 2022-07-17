using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public Upgrade upgrade;
    public List<Sprite> sprites;
    public float shake = 1;
    public int rollsLeft = 5;
    public float lockTimeOnRoll = 1f;

    private bool unlocked;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        while (!WorldGen.Instance.AllEnemiesDead)
        {
            yield return new WaitForEndOfFrame();
        }

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[rollsLeft];
        unlocked = true;
    }

    private IEnumerator OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.activeInHierarchy && unlocked && col.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            upgrade.Reroll();
            CameraFollow.Instance.Shake(shake);
            rollsLeft -= 1;
            if (rollsLeft < 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                var spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprites[rollsLeft];
            }

            unlocked = false;
            yield return new WaitForSeconds(lockTimeOnRoll);
            unlocked = true;
        }
    }
}