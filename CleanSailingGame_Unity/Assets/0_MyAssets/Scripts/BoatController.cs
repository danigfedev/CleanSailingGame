//Class description: Controls boat's movement and steering. It also simulates floating effect
using UnityEngine;

public class BoatController : MonoBehaviour
{
    #region **** Public Fields ****

    public BoatPropierties boatPropierties; //Not inbtegrated in script yet

    //Audio fields:
    public AudioSource boatAudioSource;
    public AudioClip idleClip;
    public AudioClip movingClip;

    //Boat movement fields:
    public float boatMaxSpeed = 15f;
    public float rearMaxSpeed = 5f;
    public float boatSpeedDamping = 5;
    public float steerSpeed = 1.0f;
    public float movementThreshold = 10.0f;

    //Boat rotation fields (movement + floating simulation):
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

    #endregion

    #region **** Private Fields ****

    private float throttleTimeOffset = 0;
    private Rigidbody rb;

    private bool idleSFXFlag = true;
    private bool movingSFXFlag = false;

    private float idleRotationTime = 0;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Move:
        float verticalAxis = Input.GetAxis("Vertical");
        int _boatDirectionFactor = 1;
        float _boatSpeed = boatMaxSpeed;
        if (verticalAxis < 0)
        {
            _boatDirectionFactor = -1;
            _boatSpeed = rearMaxSpeed;
        }
        
        float lerpSpeed = Mathf.Lerp(rb.velocity.magnitude * _boatDirectionFactor, _boatSpeed * verticalAxis, boatSpeedDamping);
        rb.velocity = (transform.forward) * lerpSpeed;

        //Rotation (steer and floating simulation)
        if (rb.velocity.magnitude > 0.1f)
        {
            idleRotationTime = 0; //Resetting idle rotation timing

            //If moving (fwd, bckwd) rotate around x axis
            throttleTimeOffset += Time.deltaTime;
            float xRotation = throttleRotationCurve.Evaluate(throttleTimeOffset * throttleRotationSpeed) * maxXRotation * -verticalAxis;
            xRotationController.localRotation = Quaternion.Euler(xRotation + throttleRotationOffset, xRotationController.localRotation.y, xRotationController.localRotation.z);

            //Steering:
            float horizontalAxis = Input.GetAxis("Horizontal");
            transform.Rotate((transform.up * horizontalAxis * _boatDirectionFactor) * steerSpeed * Time.fixedDeltaTime);
            
            //If steering: rotate around z axis
            float zRot = Mathf.Lerp(zRotationController.localRotation.z, -horizontalAxis * maxZSteerRotation, zRotationDamping);
            zRotationController.localRotation = Quaternion.Euler(zRotationController.localRotation.x, zRotationController.localRotation.y, zRot);
        }
        else
        {
            throttleTimeOffset = 0; //Resetting curve time
            //If not moving: sin rotation around Z axis
            idleRotationTime += Time.fixedDeltaTime;
            float rot = horizontalAmplitude * Mathf.Sin(/*Time.time*/idleRotationTime * horizontalRotSpeed);
            zRotationController.localRotation = Quaternion.Euler(zRotationController.localRotation.x, zRotationController.localRotation.y, rot);
        }

        PlaySound(verticalAxis);
    }

    //Selects right audio clip to play based on player's input
    private void PlaySound(float _verticalAxisValue)
    {
        if (Mathf.Abs(_verticalAxisValue) > 0.01f)
        {
            //Moving
            if (!movingSFXFlag)
            {
                boatAudioSource.clip = movingClip;
                boatAudioSource.Play();
                movingSFXFlag = true;
                idleSFXFlag = false;
            }
        }
        else
        {
            //Idle
            if (!idleSFXFlag)
            {
                boatAudioSource.clip = idleClip;
                boatAudioSource.Play();
                movingSFXFlag = false;
                idleSFXFlag = true;
            }
        }
    }


}
