using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStatusData", menuName = "ScriptableObjects/GameStatusData")]
public class GameStatusData : ScriptableObject
{
    public float waterHealth;
    public int cargoObjective;
    public int currentLevel;
}
