using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spaceship : MonoBehaviour
{
    public static float kmp;

    private float _forward, _rotate, _pitch, _yaw;
    private Transform _myT;

    public Rigidbody rb;
    public Vector3 rotation, position, currentVelocity;
    public float forwardSpeed, rotateSpeed, horizontalSpeed, verticalSpeed;

    void Start()
    {
        _myT = transform;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        kmp = currentVelocity.z;

        _forward = Input.GetAxisRaw("Forward");
        _rotate = Mathf.Lerp(_rotate,Input.GetAxisRaw("XRotate") * verticalSpeed, 3f * Time.deltaTime);
        _pitch = Mathf.Lerp(_pitch, Input.GetAxisRaw("YRotate") * horizontalSpeed, 2.5f * Time.deltaTime);
        _yaw = Mathf.Lerp(_yaw,Input.GetAxisRaw("ZRotate")* rotateSpeed, 3f * Time.deltaTime);

        rotation = new Vector3(_rotate, _pitch, _yaw);
    }

    void FixedUpdate()
    {
        CalculateVelocity();
        MoveShip();
    }

    void MoveShip()
    {
        // if (CurrentVelocity >= 0)
        //     _myT.position += transform.forward * (_forward * Time.deltaTime);

        if (_forward > 0)
        {
            rb.AddRelativeForce(Vector3.forward * forwardSpeed);
        }
        else if (_forward < 0 && currentVelocity.z > 0)
        {
            rb.AddRelativeForce(Vector3.back * (forwardSpeed * currentVelocity.z * Time.deltaTime));
        }
        _myT.Rotate(rotation * Time.deltaTime, Space.Self);
    }

    void CalculateVelocity()
    {
        currentVelocity = (rb.position - position) / Time.fixedDeltaTime;
        position = rb.position;
        currentVelocity = Quaternion.Euler(0, -_myT.rotation.eulerAngles.y, 0) * currentVelocity;
    }
}
