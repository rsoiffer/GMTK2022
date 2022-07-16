using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSimpleAnimation : MonoBehaviour
{
    public List<Sprite> sprites;
    public float animDelay = 1;

    private IEnumerator Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteIndex = 0;
        while (true)
        {
            yield return new WaitForSeconds(animDelay);
            spriteIndex += 1;
            spriteRenderer.sprite = sprites[spriteIndex % sprites.Count];
        }
    }
}