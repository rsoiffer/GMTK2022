using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public static WorldGen Instance;

    [Header("Prefabs")] public GameObject player;
    public GameObject wall, tree, grass;
    public GameObject door;
    public GameObject enemy;

    [Header("Generation Settings")] public int width = 20, height = 20;
    public float treeChance = .01f;
    public float grassChance = .5f;
    public int numEnemies = 10;
    public int numExtraEnemiesPerLevel = 5;

    private GameObject[,] tiles;
    private List<GameObject> allEnemies = new();

    public bool AllEnemiesDead => allEnemies.TrueForAll(e => e == null);

    private void Awake()
    {
        Instance = this;

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
                if (Random.value < treeChance)
                {
                    TrySpawn(tree, x, y);
                }

                if (Random.value < grassChance)
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