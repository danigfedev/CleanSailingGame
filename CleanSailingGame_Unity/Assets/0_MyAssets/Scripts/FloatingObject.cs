using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{

    private Rigidbody rb;
    private float initialDrag;
    private float initialRotationDrag;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialDrag = rb.drag;
        initialRotationDrag = rb.angularDrag;

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
