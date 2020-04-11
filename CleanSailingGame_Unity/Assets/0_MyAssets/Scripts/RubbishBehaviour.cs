﻿
//Class description: Handles rubbish behaviour over time (damage and destruction)

using UnityEngine;

public class RubbishBehaviour : MonoBehaviour
{
    public RubbishPropierties rubbishPropierties;
    public GameStatusData gameData;
    private BoatPropierties boatPropierties;
    public BoatPropierties BoatPropierties
    {
        get { return boatPropierties; }
        set { boatPropierties = value; }
    }
    private bool applyDamage = false;
    private float timeCounter = 0;

    private void Update()
    {
        if (applyDamage)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > 1)// apply fraction of damage every 1 sec
            {
                timeCounter = 0;
                gameData.waterHealth -= 1 / rubbishPropierties.damage;
            }
        }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            applyDamage = true;
            Destroy(transform.root.gameObject, rubbishPropierties.destructionTime);
        }

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
