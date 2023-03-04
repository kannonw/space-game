using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spaceship : MonoBehaviour
{

    public float forwardSpeed, rotateSpeed, horizontalSpeed, verticalSpeed;
    private float _forward, _rotate, _pitch, _yaw;
    public static float kmp;

    public Rigidbody rb;
    public Vector3 rotation;
    public float v;
    private bool verify;

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

        if ((int)rb.velocity.z == 0)
        {
            verify = false;
        }

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
        v = rb.velocity.z;

        if (_forward < 0 && Math.Abs(rb.velocity.z) > 0 && verify)
        {
            rb.AddRelativeForce(Vector3.back * (forwardSpeed * Math.Abs(rb.velocity.z)));
        }
        else if (_forward > 0)
        {
            rb.AddRelativeForce(Vector3.forward * forwardSpeed);
            verify = true;
        }
        
        transform.Rotate(rotation * Time.deltaTime, Space.Self);
    }
}
