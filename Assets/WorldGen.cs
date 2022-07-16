using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public static WorldGen Instance;

    public GameObject player;
    public GameObject wall, tree, grass, door;

    public int width = 20, height = 20;
    public float treeChance = .01f;
    public float grassChance = .5f;

    private GameObject[,] tiles;

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

        TrySpawn(player, Random.Range(1, width), Random.Range(1, height));
        while (!TrySpawn(door, Random.Range(1, width), Random.Range(1, height))) ;

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
    }

    private bool TrySpawn(GameObject obj, int x, int y)
    {
        if (tiles[x, y] != null) return false;
        var newObj = Instantiate(obj, transform);
        newObj.transform.position += new Vector3(x, y, 0);
        tiles[x, y] = newObj;
        return true;
    }
}