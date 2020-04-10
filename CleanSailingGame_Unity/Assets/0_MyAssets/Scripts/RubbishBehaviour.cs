using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishBehaviour : MonoBehaviour
{
    private BoatPropierties boatPropierties;
    public BoatPropierties BoatPropierties
    {
        get { return boatPropierties; }
        set { boatPropierties = value; }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.tag == "Player")
        {
            // Update points/cargo/whatever
            if(boatPropierties.currentCargo < boatPropierties.maxCargoCapacity)
            {
                Destroy(gameObject);
                boatPropierties.currentCargo++;
                Debug.Log("Rubbish picked: " + boatPropierties.currentCargo + " / " + boatPropierties.maxCargoCapacity);
            }
            else
            {
                Debug.Log("Max Cargo reached");
            }
                
        }
    }
}
