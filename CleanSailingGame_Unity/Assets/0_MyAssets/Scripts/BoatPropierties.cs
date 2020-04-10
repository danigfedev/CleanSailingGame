using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoatPropierties", menuName = "ScriptableObjects/BoatPropierties")]
public class BoatPropierties : ScriptableObject
{
    //Cargo
    public int currentCargo = 0;
    public int maxCargoCapacity = 20;//20 rubbish units

    //Movement:
    public float maxFwdSpeed = 20;
    public float maxRearSpeed = 5;
    public float maxSteerVelocity = 100;

}
