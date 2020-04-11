using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishFeedbackController : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 3);
    }
}
