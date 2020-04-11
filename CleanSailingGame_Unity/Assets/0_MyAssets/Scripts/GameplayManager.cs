using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameStatusData gameData;
    public CameraFollower cameraFollower;
    public GameObject dockController;
    public BoatController currentBoatController;
    [Range(1, 4)]
    public int gameLevel = 1;
    public float limitsMaxRadius = 102.15f;

    public float maxSpawnTime = 10;//seconds
    public int[] levelCargoObjectives;
    

    private bool playing = false;
    private int maxLevel = 4; //Hardcoded
    private float scaleFactor = 1;
    private BoatPropierties currentBoatPropierties;

    private float timeCounter = 0;

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
        //UpdateScaleFactor();
        SetGameLevel();
        ResetBoatParameters();
    }

    void Update()
    {
        if (playing)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > maxSpawnTime / gameLevel)
            {
                timeCounter = 0;
                SpawnRubbish();

            }
        }
        
    }

    [ContextMenu("Set Game level")]
    public void SetGameLevel()
    {
        gameData.currentLevel=gameLevel;
        UpdateCargoObjective();
        UpdateScaleFactor();
        UpdateWaterHealth();
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


    public void UpdateCargoFromDock()
    {
        int cargo = currentBoatPropierties.currentCargo;
        //substract cargo from objective
        gameData.cargoObjective -= cargo;
        //set cargo to zero

        if (gameData.cargoObjective <= 0)
        {
            gameLevel++;
            //Next Level
            //if level == 5 -> you win
            SetGameLevel();
        }
        ResetBoatParameters();
    }

    private void UpdateGameStatus(bool _playStatus)
    {
        cameraFollower.enabled = _playStatus;
        currentBoatController.playing = _playStatus;
        dockController.SetActive(_playStatus);
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

    private void UpdateCargoObjective()
    {
        switch (gameLevel)
        {
            case 1:
                gameData.cargoObjective = levelCargoObjectives[0];
                break;
            case 2:
                gameData.cargoObjective = levelCargoObjectives[1];
                break;
            case 3:
                gameData.cargoObjective = levelCargoObjectives[2];
                break;
            case 4:
                gameData.cargoObjective = levelCargoObjectives[3];
                break;
        }
    }

   private void SpawnRubbish()
    {
        Vector3 _point = RubbishSpawner.Instance.GetRandomPoint(limitsMaxRadius, scaleFactor);
        RubbishSpawner.Instance.InstantiateObjectAt(_point);
    }

    private void UpdateWaterHealth()
    {
        if (gameLevel == 1)
        {
            gameData.waterHealth = 100;
        }
        else
        {
            gameData.waterHealth += 15;//Recover 15% of health after completing each level
        }
    }
}
