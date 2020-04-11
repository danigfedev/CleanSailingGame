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
    private BoatPropierties currentBoatPropierties;

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
        UpdateGameStatus(playing);
        UpdateScaleFactor();
        SetGameLevel();
        ResetBoatParameters();
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
        UpdateGameStatus(playing);
        //TODO Enable Gameplay Canvas
    }

    private void UpdateGameStatus(bool _playStatus)
    {
        cameraFollower.enabled = _playStatus;
        currentBoatController.playing = _playStatus;
    }

    private void UpdateScaleFactor()
    {
        scaleFactor = (float)gameLevel / (float)maxLevel;
    }

    private void ResetBoatParameters()
    {
        currentBoatPropierties = currentBoatController.boatPropierties;
        currentBoatPropierties.currentCargo = 0;
    }
}
