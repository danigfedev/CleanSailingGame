
//Class Description: Controls camera translation and rotation, following the player's boat

using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    #region *** Public Fields ***

    public Transform target;
    public Rigidbody boatRigidBody;
    public float fwdDistance = 3f;
    public float rearDistance = 5f;
    public float cameraRelativeHeight = 0.5f;//Height relative to target
    public float damping = 5;
    public float rotationDamping = 8;
    
    #endregion

    private float distance;

    private void Start()
    {
        distance = fwdDistance;
        transform.position = target.TransformPoint(0, cameraRelativeHeight, -distance);
        transform.LookAt(target);
    }

    private void FixedUpdate()
    {
        //Calculate desired position
        Vector3 wantedPosition;
        Quaternion wantedRotation;
        float _verInput = Input.GetAxisRaw("Vertical");
        if (_verInput < 0)
        {
            _verInput = Mathf.Floor(_verInput);
            distance = rearDistance;
        }
        else
        {
            _verInput = 1;
            distance = fwdDistance;
        }
        wantedPosition = target.TransformPoint(0, cameraRelativeHeight, -distance * _verInput);
        wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);

        //Smooth translation
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.fixedDeltaTime * damping);

        //Smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);

    }
}
