//Description: Simulates water movement (up and down)

using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float speed = 1.0f;

    private void Update()
    {
        Vector3 oldPos = transform.position;
        transform.position = new Vector3(oldPos.x, Mathf.Sin(Time.time*speed) * amplitude, oldPos.z);
    }

    //Returns current water level
    public float GetWaterLevel()
    {
        return transform.position.y;
    }


    /*
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
            _collider.transform.parent.GetComponent<Rigidbody>().AddForce(waterPressure * transform.forward, ForceMode.Impulse);
        }
           
    }
    */

}
