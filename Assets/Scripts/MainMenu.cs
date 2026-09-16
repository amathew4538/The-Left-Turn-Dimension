using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Main";
    [SerializeField] private PlayableDirector cutsceneDirector;
    [SerializeField] private GameObject canvasUI;

    public void PlayGame()
    {
        Debug.Log("Play Button Clicked");

        canvasUI?.SetActive(false);

        if (cutsceneDirector != null)
        {
            cutsceneDirector.stopped += OnCutsceneEnded;
            cutsceneDirector.Play();
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void OnCutsceneEnded(PlayableDirector director)
    {
        director.stopped -= OnCutsceneEnded;
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked"); 
        Application.Quit();
    }
}