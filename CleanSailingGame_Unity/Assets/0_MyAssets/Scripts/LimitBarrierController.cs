using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBarrierController : MonoBehaviour
{

    public GameObject helpPrefab;

    //[Range(1, 4)]
    //public int gameLevel = 1;

    //public float limitsMaxRadius = 102.15f;


    public float maxStep = 2.5f;//in degrees

    //private int maxLevel = 4; //Hardcoded
    //private float scaleFactor = 1;
    private List<GameObject> limitHelpObjects = new List<GameObject>();


    #region Singleton pattern

    public static LimitBarrierController instance = null;

    // Game Instance Singleton
    public static LimitBarrierController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion


    //Draws limit circle to help user know the end of the gameplay area
    public void DrawLimits(float _limitsMaxRadius, float _scaleFactor)
    {
        //Circle eq.
        //z= sqrt((x-centerX)^2 - r^2)- centerZ

        ClearHelpObjects();
        
        //limitHelpObjects = new List<GameObject>();
        float _maxAngle = 360;// - maxStep * _scaleFactor;
        
        
        for (float _angle=0; _angle < _maxAngle; _angle += maxStep / _scaleFactor)
        {
            //Rotating around Y axis:
            Vector3 _newPosition = Quaternion.Euler(0, _angle, 0) * Vector3.right * _limitsMaxRadius * _scaleFactor;
            GameObject _helpPawn;
            if (!helpPrefab)
                _helpPawn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            else
            {
                _helpPawn = Instantiate(helpPrefab);
            }
            _helpPawn.transform.position = _newPosition;
            limitHelpObjects.Add(_helpPawn);
        }
    }

    //Adjusts limit collider's scale
    public void AdjustLimitsScale(float _scaleFactor)
    {
        transform.localScale = new Vector3(_scaleFactor, _scaleFactor, 1);
    }

    //Removes previous level help objects
    private void ClearHelpObjects()
    {
        foreach(GameObject helpObject in limitHelpObjects)
        {
            Destroy(helpObject);
        }
        limitHelpObjects.Clear();
    }
}
