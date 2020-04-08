using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Transform targetIdle;
    public Rigidbody boatRigidBody;
    //public float maxDistance = 1.5f;
    //public float minDistance = 1.5f;
    public float distance = 1.5f;
    public float cameraRelativeHeight = 0.5f;//Height relative to target
    public float cameraMinHeight = 0.25f;
    public float damping = 5;
    public float rotationDamping = 8;
    //private Vector3 idleCamPosition;
    //private float

    private void Start()
    {
        transform.position = target.TransformPoint(0, cameraMinHeight, -distance);
        //transform.position = targetIdle.TransformPoint(0, cameraRelativeHeight, -distance);
        transform.LookAt(target);
    }

    private void /*LateUpdate*/FixedUpdate()
    {

        //Calculate desired position
        Vector3 wantedPosition;
        Quaternion wantedRotation;
        if (boatRigidBody.velocity.magnitude > 0.1f)
        {
            float _verInput = Input.GetAxisRaw("Vertical");
            wantedPosition = target.TransformPoint(0, cameraRelativeHeight, -distance * _verInput);
            wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        }
        else
        {
            //wantedPosition = targetIdle.TransformPoint(0, cameraRelativeHeight, -distance);
            wantedPosition = target.TransformPoint(0, cameraMinHeight, -distance);
            wantedRotation = Quaternion.LookRotation(targetIdle.position - transform.position, target.up);
        }
        transform.position = Vector3.Lerp(transform.position, wantedPosition, /*Time.deltaTime*/Time.fixedDeltaTime * damping);

        //Smooth rotation
        //Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);

    }
}
