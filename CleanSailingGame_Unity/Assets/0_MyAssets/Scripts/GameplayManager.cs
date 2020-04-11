
//Class description: Handles gamplay core functionality

using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [HideInInspector]
    public bool playing = false;
    public GameStatusData gameData;
    public BoatPropierties currentBoatPropierties;
    public CameraFollower cameraFollower;
    public GameObject dockController;
    public BoatController currentBoatController;
    public UI_GameplayStatusMenuManager gameplayStatusCanvasController;
    [Range(1, 4)]
    public int gameLevel = 1;
    public float limitsMaxRadius = 102.15f;    

    public float maxSpawnTime = 10;//seconds
    public int[] levelCargoObjectives;

    
    private int maxLevel = 4; //Hardcoded
    private float scaleFactor = 1;
    private float initialBoatFwdSpeed;
    private float initialBoatRearSpeed;
    private float initialBoatSteerSpeed;
    private int initialBoatCargo;
    private int initialBoatMaxCapacity;
    private float timeCounter = 0;
    public float spawnTimeSpeed = 1;

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

        initialBoatFwdSpeed = currentBoatPropierties.maxFwdSpeed;
        initialBoatRearSpeed = currentBoatPropierties.maxRearSpeed;
        initialBoatSteerSpeed = currentBoatPropierties.maxSteerVelocity;
        initialBoatCargo = currentBoatPropierties.currentCargo;
        initialBoatMaxCapacity = currentBoatPropierties.maxCargoCapacity;

        ResetBoatParameters();
    }

    void Update()
    {
        if (playing)
        {
            timeCounter += Time.deltaTime * spawnTimeSpeed;
            if (timeCounter > maxSpawnTime / gameLevel)
            {
                timeCounter = 0;
                SpawnRubbish();

            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                PlayerPause();
            }

        }
        
    }

    [ContextMenu("Set Game level")]
    public void SetGameLevel()
    {
        if (gameLevel > maxLevel)
        {
            PlayerWin();
            return;
        }

        gameData.currentLevel=gameLevel;
        UpdateBoatProperties();
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

    public void ResumeGameplay()
    {
        playing = true;
        UpdateGameStatus(playing);
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
        currentBoatPropierties.currentCargo = 0;
        //ResetBoatParameters();
    }

    public void PlayerWin()
    {
        playing = false;
        UpdateGameStatus(playing);
        gameplayStatusCanvasController.SetTitleText("You Win");
        gameplayStatusCanvasController.restartButton.SetActive(true);
        gameplayStatusCanvasController.gameObject.SetActive(true);
    }

    public void PlayerLose()
    {
        playing = false;
        UpdateGameStatus(playing);
        gameplayStatusCanvasController.SetTitleText("You Lose");
        gameplayStatusCanvasController.restartButton.SetActive(true);
        gameplayStatusCanvasController.gameObject.SetActive(true);
    }

    private void PlayerPause()
    {
        playing = false;
        UpdateGameStatus(playing);
        gameplayStatusCanvasController.SetTitleText("Pause");
        gameplayStatusCanvasController.resumeButton.SetActive(true);
        gameplayStatusCanvasController.gameObject.SetActive(true);
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

    public void ResetBoatParameters()
    {
        currentBoatPropierties.maxFwdSpeed = initialBoatFwdSpeed;
        currentBoatPropierties.maxRearSpeed = initialBoatRearSpeed;
        currentBoatPropierties.maxSteerVelocity = initialBoatSteerSpeed;
        currentBoatPropierties.currentCargo = initialBoatCargo;
        currentBoatPropierties.maxCargoCapacity = initialBoatMaxCapacity;
    }

    private void UpdateBoatProperties()
    {
        if (gameLevel > 1)
        {
            currentBoatPropierties.maxCargoCapacity += 5;
            currentBoatPropierties.maxFwdSpeed += 5;
        }

        //Update Objective (now fixed by user)
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
            gameData.waterHealth += 20;//Recover 15% of health after completing each level
            if (gameData.waterHealth > 100) gameData.waterHealth = 100;
        }
    }
}
