using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Vector3 CenterOfMass;
    [Space(15)]
    public float boatSpeed = 1.0f;
    public float steerSpeed = 1.0f;
    public float movementThreshold = 10.0f;


    private Transform m_CenterOfMass;
    private float verticalInput;
    private float movementFactor;
    private float horizontalInput;
    private float steerFactor;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    //private void Update()
    //{
    //    Balance();
    //    Movement();
    //    Steering();
    //}

    private void Balance()
    {
        if (!m_CenterOfMass)
        {
            m_CenterOfMass = new GameObject("CenterOfMass").transform;
            m_CenterOfMass.SetParent(transform);
        }
        m_CenterOfMass.position = CenterOfMass;
        rb.centerOfMass = m_CenterOfMass.position;
    }

    private void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        movementFactor = verticalInput;//Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold);
        Debug.Log(movementFactor);
        transform.Translate(0, 0, movementFactor * boatSpeed);
    }

    private void Steering()
    {
        horizontalInput= Input.GetAxis("Horizontal");
        steerFactor = Mathf.Lerp(steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThreshold);
        transform.Rotate(0, steerFactor * steerSpeed, 0);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Balance();
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        rb.velocity = (transform.forward * verticalAxis) * boatSpeed;// * Time.fixedDeltaTime;//new Vector3(rb.velocity.x, rb.velocity.y, verticalAxis * boatSpeed);
        transform.Rotate((transform.up * horizontalAxis) * steerSpeed * Time.fixedDeltaTime);
    }


}
