using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public static WorldGen Instance;

    [Header("Prefabs")] public GameObject player;
    public GameObject wall, water;
    public GameObject tree, grass, pebble;
    public GameObject door;
    public GameObject enemy;

    [Header("Generation Settings")] public int width = 20;
    public int height = 20;
    public float treeChance = .1f;
    public float treeChanceIncrease = .05f;
    public float grassChance = .5f;
    public float pebbleChance = .01f;
    public int numEnemies = 10;
    public int numExtraEnemiesPerLevel = 5;

    private GameObject[,] tiles;
    private List<GameObject> allEnemies = new();

    public bool AllEnemiesDead => allEnemies.TrueForAll(e => e == null);

    private void Start()
    {
        Instance = this;
        int seed = Random.Range(0, 1000);

        tiles = new GameObject[width + 1, height + 1];
        for (int x = 0; x <= width; x++)
        {
            TrySpawn(wall, x, 0);
            TrySpawn(wall, x, height);
        }

        for (int y = 0; y <= height; y++)
        {
            TrySpawn(wall, 0, y);
            TrySpawn(wall, width, y);
        }

        for (int x = 1; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                var distToCenter = new Vector2(x - .5f * width, y - .5f * height).magnitude;
                var elevation = NoiseHelper.FBM(x, y, 2, .02f, seed - 20);

                var newWallChance = -.1 + .05 * distToCenter;
                if (NoiseHelper.FBM(x, y, 2, .2f, seed - 10) < newWallChance)
                {
                    if (elevation > .5)
                    {
                        TrySpawn(wall, x, y);
                    }
                    else
                    {
                        TrySpawn(water, x, y);
                    }
                }

                if (Random.value < pebbleChance)
                {
                    TrySpawn(pebble, x, y);
                }

                var newTreeChance = treeChance + distToCenter * treeChanceIncrease;
                if (NoiseHelper.FBM(x, y, 2, .2f, seed) < newTreeChance)
                {
                    TrySpawn(tree, x, y);
                }

                if (NoiseHelper.FBM(x, y, 2, .2f, seed + 10) < grassChance)
                {
                    TrySpawn(grass, x, y);
                }
            }
        }

        while (!TrySpawn(player, Random.Range(1, width), Random.Range(1, height))) ;
        while (!TrySpawn(door, Random.Range(1, width), Random.Range(1, height))) ;
        var currentNumEnemies = numEnemies + numExtraEnemiesPerLevel * LevelManager.Instance.levelNum;
        for (int i = 0; i < currentNumEnemies; i++)
        {
            GameObject newEnemy = null;
            while (newEnemy == null)
            {
                newEnemy = TrySpawn(enemy, Random.Range(1, width), Random.Range(1, height));
            }

            allEnemies.Add(newEnemy);
        }
    }

    private GameObject TrySpawn(GameObject obj, int x, int y)
    {
        if (tiles[x, y] != null) return null;
        var newObj = Instantiate(obj, transform);
        newObj.transform.position += new Vector3(x, y, 0);
        tiles[x, y] = newObj;
        return newObj;
    }
}