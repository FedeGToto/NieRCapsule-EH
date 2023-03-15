using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCamera : MonoBehaviour
{
    Transform cameraTrans;

    private void Start()
    {
        cameraTrans = Camera.main.transform;
    }

    private void Update()
    { 
        transform.LookAt(2 * transform.position - cameraTrans.position);
    }
}
