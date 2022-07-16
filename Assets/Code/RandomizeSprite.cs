using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    public List<Sprite> sprites;

    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}