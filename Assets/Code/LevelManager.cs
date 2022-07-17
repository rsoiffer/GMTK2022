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

    public int upgradeWater1;
    public int upgradeWater2;
    public int upgradeWater3;
    public int upgradeWater4;

    public int upgradeEarth1;
    public int upgradeEarth2;
    public int upgradeEarth3;
    public int upgradeEarth4;

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
            case 4:
                upgradeWater1++;
                break;
            case 5:
                upgradeWater2++;
                break;
            case 6:
                upgradeWater3++;
                break;
            case 7:
                upgradeWater4++;
                break;
            case 8:
                upgradeEarth1++;
                break;
            case 9:
                upgradeEarth2++;
                break;
            case 10:
                upgradeEarth3++;
                break;
            case 11:
                upgradeEarth4++;
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