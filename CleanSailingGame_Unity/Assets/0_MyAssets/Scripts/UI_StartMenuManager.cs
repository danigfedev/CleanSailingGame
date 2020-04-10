
//Class description: Controls game's Start Menu 

using UnityEngine;

public class UI_StartMenuManager : MonoBehaviour
{
    public void PlayButton_OnClick()
    {
        GameplayManager.Instance.StartGame();
        Destroy(gameObject);
    }
}
