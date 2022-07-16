using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public GameObject wall, tree;

    public int width = 20, height = 20;
    public float treeChance = .01f;

    private void Start()
    {
        for (int x = 0; x <= width; x++)
        {
            var myWall = Instantiate(wall);
            myWall.transform.position = new Vector2(x, 0);
            myWall = Instantiate(wall);
            myWall.transform.position = new Vector2(x, height);
        }

        for (int y = 0; y <= height; y++)
        {
            var myWall = Instantiate(wall);
            myWall.transform.position = new Vector2(0, y);
            myWall = Instantiate(wall);
            myWall.transform.position = new Vector2(width, y);
        }

        for (int x = 1; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                if (Random.value < treeChance)
                {
                    var myTree = Instantiate(tree);
                    myTree.transform.position = new Vector2(x, y);
                }
            }
        }
    }
}