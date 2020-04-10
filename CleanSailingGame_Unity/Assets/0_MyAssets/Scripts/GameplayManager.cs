using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public CameraFollower cameraFollower;
    public BoatController currentBoatController;
    [Range(1, 4)]
    public int gameLevel = 1;
    public float limitsMaxRadius = 102.15f;

    private bool playing = false;
    private int maxLevel = 4; //Hardcoded
    private float scaleFactor = 1;

    #region Singleton pattern

    public static GameplayManager instance = null;

    // Game Instance Singleton
    public static GameplayManager Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion

    public void Awake()
    {
        instance = this;
        //Disable boat controller and camera follower
        cameraFollower.enabled = playing;
        currentBoatController.enabled = playing;
        UpdateScaleFactor();
        SetGameLevel();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Trying to instantiate");
            Vector3 _point = RubbishSpawner.Instance.GetRandomPoint(limitsMaxRadius, scaleFactor);
            RubbishSpawner.Instance.InstantiateObjectAt(_point);
        }
    }

    [ContextMenu("Set Game level")]
    public void SetGameLevel()
    {
        UpdateScaleFactor();

        //Limits: SetGameLevel
        LimitBarrierController.Instance.DrawLimits(limitsMaxRadius, scaleFactor);
        LimitBarrierController.Instance.AdjustLimitsScale(scaleFactor);

    }


    public void StartGame()
    {
        playing = true;
        // Enable boat controller and camera follower
        cameraFollower.enabled = playing;
        currentBoatController.enabled = playing;
        //TODO Enable Gameplay Canvas

    }

    private void UpdateScaleFactor()
    {
        scaleFactor = (float)gameLevel / (float)maxLevel;
    }
}
