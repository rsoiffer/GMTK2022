﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Door door;
    public GameObject die;
    public List<Sprite> sprites;
    public float shake = 1;

    public List<int> optionsOverride;

    private bool unlocked;
    private int id;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        while (!WorldGen.Instance.AllEnemiesDead)
        {
            yield return new WaitForEndOfFrame();
        }

        id = Random.Range(0, sprites.Count);

        if (optionsOverride.Count != 0)
        {
            id = optionsOverride[Random.Range(0, optionsOverride.Count)];
        }

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[id];
        unlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.activeInHierarchy && unlocked && col.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.Upgrade(id);
            gameObject.SetActive(false);
            die.SetActive(true);
            door.Unlock();
            CameraFollow.Instance.Shake(shake);
        }
    }
}