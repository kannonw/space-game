using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public static float Kpm;
    public static bool ComputerMovement;
    public bool OnOrbit;

    [SerializeField] private float forwardSpeed, backwardSpeed, rotateSpeed;
    [SerializeField] private GameInput gameInput;
    
    private float _activeForwardSpeed, _forward, _forwardVelocity, _speedLimit;
    private Vector3 _rotation, _lastPosition, _lastRotation;
    private Transform _objTransform;
    private Rigidbody _rb;

    private void Start()
    {
        _objTransform = transform;
        _rb = GetComponent<Rigidbody>();
    }
    
    
    private void FixedUpdate()
    {
        CalculateVelocity();
        HandleMovement();
    }
    

    private void HandleMovement()
    {
        _forward = gameInput.GetMovementStraight();
        _rotation = gameInput.GetMovementRotate();

        if (_forward != 0)
        {
            _rb.velocity = Vector3.zero;
            OnOrbit = false;
        }

        if (_forward < 0)
            _speedLimit = backwardSpeed;
        else
            _speedLimit = forwardSpeed;
        
        float forwardTimeFactor = .6f;
        float rotationTimeFactor = 2f;

        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed, _forward * _speedLimit, Time.deltaTime * forwardTimeFactor);
        _objTransform.position += _objTransform.forward * (_activeForwardSpeed * Time.deltaTime);
        
        _rotation = new Vector3(
            Mathf.Lerp(_lastRotation.x, _rotation.x * rotateSpeed, Time.deltaTime * rotationTimeFactor),
            Mathf.Lerp(_lastRotation.y, _rotation.y * rotateSpeed, Time.deltaTime * rotationTimeFactor),
            Mathf.Lerp(_lastRotation.z, _rotation.z * rotateSpeed, Time.deltaTime * rotationTimeFactor)
        );
        
        _objTransform.Rotate(_rotation * (rotateSpeed * Time.deltaTime), Space.Self);
        _lastRotation = _rotation;
    }
    

    private void CalculateVelocity()
    {
        Vector3 currentPos = _objTransform.position;
        _forwardVelocity = Vector3.Dot((currentPos - _lastPosition) / Time.fixedDeltaTime, _objTransform.forward);
        _lastPosition = currentPos;
        
        Kpm = _forwardVelocity / (Time.fixedDeltaTime * 100f);
        
        // currentVelocity = (rb.position - position) / Time.fixedDeltaTime;
        // currentVelocity = Quaternion.Euler(0, -_myT.rotation.eulerAngles.y, 0) * currentVelocity;
        // forwardVelocity = Vector3.Dot(rb.velocity, _myT.forward);
    }


    // void MoveShip()
    // {
    //     if (_forward > 0)
    //     {
    //         rb.AddRelativeForce(Vector3.forward * (forwardSpeed * Time.deltaTime), ForceMode.Force);
    //     }
    //     else if (_forward < 0 && currentVelocity.z > 0)
    //     {
    //         rb.AddRelativeForce(Vector3.back * (forwardSpeed * currentVelocity.z * Time.deltaTime), ForceMode.Force);
    //     }
    // }
}
