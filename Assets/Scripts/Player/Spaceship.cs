using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spaceship : MonoBehaviour
{

    public float fowardSpeed, rotateSpeed, horizontalSpeed, verticalSpeed;
    private float _forward, _rotate, _pitch, _yaw;
    public static float kmp;

    public Rigidbody rb;
    public Vector3 rotation, movement;

    void Start()
    {
         rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        kmp = rb.velocity.magnitude;

        _forward = Input.GetAxisRaw("Forward");
        _rotate = Input.GetAxisRaw("XRotate");
        _pitch = Input.GetAxisRaw("YRotate");
        _yaw = Input.GetAxisRaw("ZRotate");

        
        
        // movement = new Vector3(Input.GetAxisRaw("Forward"), 0, 0);
        // movement = transform.rotation.eulerAngles * Input.GetAxisRaw("Forward");
        // rotation = new Vector3(Input.GetAxis("XRotate") * verticalSpeed, Input.GetAxis("YRotate") * horizontalSpeed, Input.GetAxis("ZRotate") * rotateSpeed);
    }

    void FixedUpdate()
    {
        MoveShip();
    }

    void MoveShip()
    {
        rotation = new Vector3(_rotate * verticalSpeed, _pitch * horizontalSpeed, _yaw * rotateSpeed);
        movement = transform.rotation.eulerAngles.normalized;

        if (_forward < 0)
            rb.AddRelativeForce(-movement * (fowardSpeed * ((int)rb.velocity.magnitude / movement.magnitude)));
        else
            rb.AddRelativeForce(movement * (fowardSpeed * _forward));
        
        transform.Rotate(rotation * Time.deltaTime, Space.Self);
    }
    
    // movement = Vector3.forward * _forward;
    // rotation = movement * (fowardSpeed * (rb.velocity.magnitude / movement.magnitude));
    // if (_forward < 0)
    //     rb.AddRelativeForce(movement * (fowardSpeed * (rb.velocity.magnitude / movement.magnitude)));
    // else
    // rb.AddRelativeForce(movement);
    //     
    // transform.Rotate(new Vector3(_rotate * verticalSpeed, _pitch * horizontalSpeed, _yaw * rotateSpeed) * Time.deltaTime, Space.Self);
}
