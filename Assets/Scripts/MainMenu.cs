using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string gameSceneName = "Main";
    [SerializeField] private PlayableDirector cutsceneDirector;
    [SerializeField] private GameObject canvasUI;

    [Header("UI & Screen Fade")]
    public CanvasGroup titleScreenUI;
    public CanvasGroup blackFadeOverlay;
    public TextMeshProUGUI timeSkipText;

    [Header("Next Scene Settings")]
    public string raceName = "Monte Carlo Rally";
    public string dateDisplay = "JANUARY 21, 2027";
    public string timeDisplay = "14:01 PM CET";

    [ContextMenu("Play Game")]
    public void PlayGame()
    {
        Debug.Log("Play Button Clicked");

        canvasUI?.SetActive(false);

        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    [ContextMenu("Quit Game")]
    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }

    [ContextMenu("Trigger Time Skip")]
    public void TriggerTimeScreen()
    {
        StartCoroutine(StartTimeScreen());
    }

    public IEnumerator StartTimeScreen()
    {
        if (timeSkipText != null)
        {
            timeSkipText.text = $"{raceName}\n{dateDisplay}\n{timeDisplay}";
        }

        float timer = 0f;
        blackFadeOverlay.blocksRaycasts = true;
        while (timer < 0.1f)
        {
            timer += Time.deltaTime;
            if (blackFadeOverlay != null)
            {
                blackFadeOverlay.alpha = Mathf.Clamp01(timer / 0.1f);
            }
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(gameSceneName);
    }
}