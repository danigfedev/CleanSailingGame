using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockController : MonoBehaviour
{
    public GameObject Canvas;
    public AudioSource dockAudioSource;
    private bool playerInside = false;
    
    private void Start()
    {
        LeanTween.scale(Canvas, Canvas.transform.localScale * 1.2f, 0.5f).setLoopPingPong();
    }

    private void Update()
    {
        Canvas.transform.LookAt(Camera.main.transform);
        if (playerInside && Input.GetKeyUp(KeyCode.E))
        {
            dockAudioSource.Play();
            GameplayManager.instance.UpdateCargoFromDock();
        }

    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.tag == "Player")
        {
            Canvas.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.tag == "Player")
        {
            Canvas.SetActive(false);
            playerInside = false;
        }

    }

    //private void OnTriggerStay(Collider _collider)
    //{
    //    // if player presses E
    //    if (_collider.tag == "Player")
    //    {
    //        playerInside = true;
            
    //    }

    //}
}
