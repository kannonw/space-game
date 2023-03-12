using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spaceship : MonoBehaviour
{

    public float forwardSpeed, rotateSpeed, horizontalSpeed, verticalSpeed;
    public static float kmp;
    private float _forward, _rotate, _pitch, _yaw;
    private Transform _myT;

    public Rigidbody rb;
    public Vector3 rotation;
    // public float v;
    // private bool verify;

    void Start()
    {
         rb = GetComponent<Rigidbody>();
         _myT = transform;
    }
    
    void Update()
    {
        kmp = rb.velocity.magnitude;

        _forward = Mathf.Lerp(_forward, Input.GetAxisRaw("Forward") * forwardSpeed, 2f * Time.deltaTime);
        _rotate = Mathf.Lerp(_rotate,Input.GetAxisRaw("XRotate") * verticalSpeed, 4f * Time.deltaTime);
        _pitch = Mathf.Lerp(_pitch, Input.GetAxisRaw("YRotate") * horizontalSpeed, 3f * Time.deltaTime);
        _yaw = Mathf.Lerp(_yaw,Input.GetAxisRaw("ZRotate")* rotateSpeed, 4f * Time.deltaTime);

        rotation = new Vector3(_rotate, _pitch, _yaw);
        // if ((int)rb.velocity.z == 0)
        // {
        //     verify = false;
        // }

        // movement = new Vector3(Input.GetAxisRaw("Forward"), 0, 0);
        // movement = transform.rotation.eulerAngles * Input.GetAxisRaw("Forward");
    }

    void FixedUpdate()
    {
        MoveShip();
    }

    void MoveShip()
    {
        if (rb.velocity.z >= 0)
            _myT.position += transform.forward * (_forward * Time.deltaTime);
        _myT.Rotate(rotation * Time.deltaTime, Space.Self);
        // rotation = new Vector3(_rotate * verticalSpeed, _pitch * horizontalSpeed, _yaw * rotateSpeed);
        // v = rb.velocity.z;
        //
        // if (_forward < 0 && Math.Abs(rb.velocity.z) > 0 && verify)
        // {
        //     rb.AddRelativeForce(Vector3.back * (forwardSpeed * Math.Abs(rb.velocity.z)));
        // }
        // else if (_forward > 0)
        // {
        //     rb.AddRelativeForce(Vector3.forward * forwardSpeed);
        //     verify = true;
        // }
        //
    }
}
