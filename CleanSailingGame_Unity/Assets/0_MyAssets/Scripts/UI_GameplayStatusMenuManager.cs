
//Class description: Controls game's Start Menu 

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameplayStatusMenuManager : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject resumeButton;
    public GameObject quitButton;
    public TextMeshProUGUI titleText;
    public float desiredScale = 5;

    private void OnEnable()
    {
        LeanTween.scale(restartButton, Vector3.one * desiredScale, 1).setLoopPingPong();
    }

    private void OnDisable()
    {
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
    }

    public void SetTitleText(string _title)
    {
        titleText.text = _title;
    }

    public void RestartButton_OnClick()
    {
        GameplayManager.Instance.ResetBoatParameters();//StartGame();
        //Load this scene again...
        //Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    public void ResumeButton_OnClick()
    {
        GameplayManager.Instance.ResumeGameplay();
        gameObject.SetActive(false);
    }

    public void ExitButton_OnClink()
    {
        GameplayManager.Instance.ResetBoatParameters();//StartGame();s
        //Application.Quit();
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
