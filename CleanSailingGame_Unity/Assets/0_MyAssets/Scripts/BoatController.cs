using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    
    public float boatMaxSpeed = 1.0f;
    public float boatSpeedDamping = 5;
    public float steerSpeed = 1.0f;
    public float movementThreshold = 10.0f;

    public Transform zRotationController;
    public Transform xRotationController;
    public float horizontalAmplitude = 5;
    public float horizontalRotSpeed = 1;

    
    public float maxZSteerRotation = 10;
    public float zRotationDamping = 10;

    public float maxXRotation = 10;//In degrees
    public float xRotationDamping = 10;
    public AnimationCurve throttleRotationCurve;
    public float throttleRotationSpeed = 0.75f;
    public float throttleRotationOffset = 2;
    private float throttleTimeOffset = 0;
    private Rigidbody rb;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");

        //Move:
        //TODO Lerp velocity to add certain drag to movement
        float lerpSpeed = Mathf.Lerp(rb.velocity.magnitude, boatMaxSpeed * verticalAxis, boatSpeedDamping);
        rb.velocity = (transform.forward /*verticalAxis*/) * lerpSpeed;// * Time.fixedDeltaTime;//new Vector3(rb.velocity.x, rb.velocity.y, verticalAxis * boatSpeed);

        if (rb.velocity.magnitude > 0.1f)
        {
            //If moving (fwd, bckwd) rotate around x axis
            throttleTimeOffset += Time.deltaTime;
            float xRotation = throttleRotationCurve.Evaluate(throttleTimeOffset * throttleRotationSpeed) * maxXRotation * -verticalAxis;

            xRotationController.localRotation = Quaternion.Euler(xRotation + throttleRotationOffset, xRotationController.localRotation.y, xRotationController.localRotation.z);

            //Rotate boat:
            float horizontalAxis = Input.GetAxis("Horizontal");
            transform.Rotate((transform.up * horizontalAxis) * steerSpeed * Time.fixedDeltaTime);
            //If steering: rotate around z axis

            float zRot = Mathf.Lerp(zRotationController.localRotation.z, -horizontalAxis * maxZSteerRotation, zRotationDamping);
            zRotationController.localRotation = Quaternion.Euler(zRotationController.localRotation.x, zRotationController.localRotation.y, zRot);
        }
        else
        {
            throttleTimeOffset = 0; //Resetting curve time
            //If not moving: sin rotation around Z axis
            float rot = horizontalAmplitude * Mathf.Sin(Time.time * horizontalRotSpeed);
            zRotationController.localRotation = Quaternion.Euler(zRotationController.localRotation.x, zRotationController.localRotation.y, rot);
        }
    }


}
