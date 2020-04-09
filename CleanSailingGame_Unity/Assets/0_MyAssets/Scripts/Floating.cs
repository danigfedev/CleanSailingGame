using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

    public float verticalRotSpeed = 1;
    public float horizontalRotSpeed = 1;
    public float horizontalAmplitude = 10;
    WaterBehaviour water;


    void Start()
    {
        water = GameObject.Find("Water").GetComponent<WaterBehaviour>();
    }

    void Update()
    {
        //Move with water:
        Vector3 oldPos = transform.position;
        transform.position = new Vector3(transform.position.x, water.GetWaterLevel(), transform.position.z);

        //Rotate:
        transform.Rotate(Vector3.up, verticalRotSpeed * Time.deltaTime, Space.World);

        float rot = horizontalAmplitude * Mathf.Sin(Time.time * horizontalRotSpeed);
        transform.GetChild(0).localRotation = Quaternion.Euler(rot, transform.localRotation.y, transform.localRotation.z);

    }



}
