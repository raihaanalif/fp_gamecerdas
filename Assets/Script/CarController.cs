using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float moveSpeed = 10;
    private float turnSpeed = 2;

    private Rigidbody rb;
    private float gravity = -9.81f;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
        Move();
        Turn();
        Falls();
    }

    void Move(){
        if(Input.GetKey(KeyCode.W)){
            rb.AddRelativeForce(Vector3.forward * moveSpeed);
        }
        if(Input.GetKey(KeyCode.S)){
            rb.AddRelativeForce(-(Vector3.forward * moveSpeed)/2);
        }
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x = 0;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    void Turn(){
        if(Input.GetKey(KeyCode.D)){
            rb.AddTorque(Vector3.up * turnSpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            rb.AddTorque(-(Vector3.up * turnSpeed));
        }
    }

    void Falls(){
        rb.AddForce(Vector3.down * gravity);
    }
}
