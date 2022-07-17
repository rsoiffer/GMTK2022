using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleMenuView : MonoBehaviour
{
    private void OnEnable()
    {
        var document = GetComponent<UIDocument>();

        var playButton = document.rootVisualElement.Q<Button>("PlayButton");
        playButton.clicked += () => {
            GetComponent<AudioSource>().Play();
            SceneManager.LoadScene("Level");
        };

        var quitButton = document.rootVisualElement.Q<Button>("QuitButton");
        quitButton.clicked += () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        };
    }
}