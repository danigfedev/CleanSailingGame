
//Class description: Controls game's Start Menu 

using UnityEngine;

public class UI_StartMenuManager : MonoBehaviour
{
    public GameObject startButton;
    public float desiredScale = 5;

    private void OnEnable()
    {
        LeanTween.scale(startButton, Vector3.one * desiredScale, 1).setLoopPingPong();
    }
    public void PlayButton_OnClick()
    {
        GameplayManager.Instance.StartGame();
        Destroy(gameObject);
    }
}
