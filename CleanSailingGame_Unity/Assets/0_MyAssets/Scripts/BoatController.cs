using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public float boatSpeed = 50;
    public float rotationSpeed = 25;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        Debug.Log(verticalAxis);
        rb.velocity = (transform.forward * verticalAxis) * boatSpeed; // * Time.deltaTime;//new Vector3(rb.velocity.x, rb.velocity.y, verticalAxis * boatSpeed);
        transform.Rotate((transform.up * horizontalAxis) * rotationSpeed * Time.fixedDeltaTime);
    }
}
