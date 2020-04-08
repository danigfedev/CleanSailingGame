using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public float boatSpeed = 50;
    public float rotationSpeed = 25;

    private Rigidbody rb;
    private float initialDrag;
    private float initialRotationDrag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialDrag = rb.drag;
        initialRotationDrag = rb.angularDrag;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        rb.velocity = (transform.forward * verticalAxis) * boatSpeed;// * Time.fixedDeltaTime;//new Vector3(rb.velocity.x, rb.velocity.y, verticalAxis * boatSpeed);
        transform.Rotate((transform.up * horizontalAxis) * rotationSpeed * Time.fixedDeltaTime);
    }

    public void ResetColliderValues()
    {
        rb.drag = initialDrag;
        rb.angularDrag = initialRotationDrag;
    }

    public void OverrideColliderValues(float _drag, float _angularDrag)
    {
        rb.drag = _drag;
        rb.angularDrag = _angularDrag;
    }
}
