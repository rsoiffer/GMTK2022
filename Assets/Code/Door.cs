using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openSprite;
    private bool unlocked;

    private IEnumerator Start()
    {
        while (!WorldGen.Instance.AllEnemiesDead)
        {
            yield return new WaitForEndOfFrame();
        }

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = openSprite;
        unlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (unlocked && col.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.ToNextLevel();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LevelManager.Instance.ToNextLevel();
        }
    }
}