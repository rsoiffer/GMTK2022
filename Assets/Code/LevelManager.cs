using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int levelNum;

    public int upgradeFire1;
    public int upgradeFire2;
    public int upgradeFire3;
    public int upgradeFire4;

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
        switch (choice)
        {
            case -1:
                // TODO
                break;
            case 0:
                upgradeFire1++;
                break;
            case 1:
                upgradeFire2++;
                break;
            case 2:
                upgradeFire3++;
                break;
            case 3:
                upgradeFire4++;
                break;
        }

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