using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int levelNum;

    private int choice = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ToNextLevel()
    {
        levelNum += 1;
        SceneManager.LoadScene("Level");
    }

    public void Reroll()
    {
        choice = -1;
    }

    public void Upgrade(int id)
    {
        choice = id;
    }
}