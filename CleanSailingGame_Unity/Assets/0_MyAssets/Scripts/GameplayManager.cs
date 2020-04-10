using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [Range(1, 4)]
    public int gameLevel = 1;
    public float limitsMaxRadius = 102.15f;
    private int maxLevel = 4; //Hardcoded
    private float scaleFactor = 1;


    public void Awake()
    {
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

    private void UpdateScaleFactor()
    {
        scaleFactor = (float)gameLevel / (float)maxLevel;
    }
}
