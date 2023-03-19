using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spaceship : MonoBehaviour
{
    public static float kmp;
    public static bool ComputerMovement;

    private Transform _myT;
    
    private float _forward, _activeForwardSpeed, _rotate, _pitch, _yaw;
    public float forwardSpeed, rotateSpeed, horizontalSpeed, verticalSpeed;

    public Rigidbody rb;
    public Vector3 rotation, position, currentVelocity;

    void Start()
    {
        _myT = transform;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ComputerMovement = !ComputerMovement;
        }

        kmp = currentVelocity.z;
        _forward = Input.GetAxis("Forward");
        _rotate = Mathf.Lerp(_rotate,Input.GetAxisRaw("XRotate") * verticalSpeed, 3f * Time.deltaTime);
        _pitch = Mathf.Lerp(_pitch, Input.GetAxisRaw("YRotate") * horizontalSpeed, 2.5f * Time.deltaTime);
        _yaw = Mathf.Lerp(_yaw,Input.GetAxisRaw("ZRotate")* rotateSpeed, 3f * Time.deltaTime);

        rotation = new Vector3(_rotate, _pitch, _yaw);
    }

    void FixedUpdate()
    {
        CalculateVelocity();
        _myT.Rotate(rotation * Time.deltaTime, Space.Self);
        
        if (ComputerMovement)
            LinearMoveShip();
        else
            MoveShip();
    }

    void MoveShip()
    {
        if (_forward > 0)
        {
            rb.AddRelativeForce(Vector3.forward * (forwardSpeed * Time.deltaTime), ForceMode.Force);
        }
        else if (_forward < 0 && currentVelocity.z > 0)
        {
            rb.AddRelativeForce(Vector3.back * (forwardSpeed * currentVelocity.z * Time.deltaTime), ForceMode.Force);
        }
    }

    void LinearMoveShip()
    {
        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed, forwardSpeed * _forward, Time.deltaTime);
        
        _myT.position += _myT.forward * (_activeForwardSpeed * Time.deltaTime);
    }

    void CalculateVelocity()
    {
        currentVelocity = (rb.position - position) / Time.fixedDeltaTime;
        position = rb.position;
        currentVelocity = Quaternion.Euler(0, -_myT.rotation.eulerAngles.y, 0) * currentVelocity;
    }
}
