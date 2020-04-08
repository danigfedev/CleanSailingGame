using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public float waterPressure = 3f;
    public float waterDrag = 1;
    public float angularDrag = 1.0f;

    //private collider

    private void Start()
    {
        waterPressure = waterPressure / 10f;
    }

    private void OnTriggerEnter(Collider _collider)
    {


        if(_collider.tag == "Float")
        {
            Debug.Log(_collider.name + " entered the water");
            _collider.transform.parent.GetComponent<FloatingObject>().OverrideColliderValues(waterDrag, angularDrag);
        }
        


        //Get boat's collider initial values

    }


    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.tag == "Float")
        {
            Debug.Log(_collider.name + " left the water");
            _collider.transform.parent.GetComponent<FloatingObject>().ResetColliderValues();
        }
           
        //ResetCollider(_collider);
    }


    private void OnTriggerStay(Collider _collider)
    {
        if (_collider.tag == "Float")
        {
            //Add forces 

            //Vector3 forceDir = transform.forward + new Vector3(Random.Range(-1, 2)*0.01f, Random.Range(-1, 2), 0);
            _collider.transform.parent.GetComponent<Rigidbody>().AddForce(waterPressure * /*forceDir*/transform.forward, ForceMode.Impulse);
        }
           
    }
}
