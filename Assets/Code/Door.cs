using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openSprite;
    private bool unlocked;

    public void Unlock()
    {
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