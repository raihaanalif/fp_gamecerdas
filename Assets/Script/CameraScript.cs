using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void update ()
    {
        transform.position = target.position + offset;
    }
}
