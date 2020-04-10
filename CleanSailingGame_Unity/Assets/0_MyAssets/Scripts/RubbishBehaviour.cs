using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _collider)
    {
        Debug.Log("Something entered");

        if (_collider.tag == "Player")
        {
            //TODO Update points/cargo/whatever
            //if(!maxCapacityReached) pick rubbish
            Destroy(gameObject);
        }
    }
}
