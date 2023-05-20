using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipInteraction : MonoBehaviour
{
    [SerializeField] private GravitationalAttraction spaceshipGravitationalAttraction;
    [SerializeField] private Spaceship spaceshipController;
    [SerializeField] private int orbitMinDistance;

    private Rigidbody _rb;

    private Vector3 _bodyPosition;
    private float _bodyDistance, _bodyMass, _rotateAroundSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CalculateDistance();
        RotateAround();
        Debug.Log(_bodyDistance);
        Debug.Log(spaceshipGravitationalAttraction.beAttracted);
    }


    private void CalculateDistance()
    {
        _bodyDistance = 0f;
        foreach (GravitationalAttraction obj in GravitationalAttraction.Attractors)
        {
            if (obj == spaceshipGravitationalAttraction) continue;
            
            Vector3 distanceFromObj = obj.transform.position - transform.position;
            if (_bodyDistance < distanceFromObj.magnitude && _bodyDistance != 0) continue;

            _bodyDistance = distanceFromObj.magnitude;
            _bodyPosition = obj.transform.position;
            _bodyMass = obj.rb.mass;
        }
    }

    private void RotateAround()
    {
        if (spaceshipController.OnOrbit)
        {
            // float angle = Mathf.Atan2(transform.position.y - _bodyPosition.y, transform.position.z - _bodyPosition.z) * Mathf.Rad2Deg;
            // Quaternion targetRotation = Quaternion.Euler(-angle, 0f, 0f);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 10f);
            
            // transform.Rotate(Vector3.right, angle * Time.deltaTime);
            
            transform.RotateAround(_bodyPosition, transform.up, Time.deltaTime * _rotateAroundSpeed);
            
            // float angle = Vector3.Angle(_bodyPosition, transform.forward);
            // Debug.Log(Mathf.Cos(angle));   
        }
  

        if (!(Input.GetKeyDown("space") && _bodyDistance <= orbitMinDistance)) return;
        Debug.Log("On orbit");

        spaceshipController.OnOrbit = true;
        spaceshipGravitationalAttraction.beAttracted = false;
        _rotateAroundSpeed = Mathf.Sqrt(GravitationalAttraction.G * (_bodyMass + _rb.mass) / _bodyDistance) / 4;
        _rb.velocity = Vector3.zero;

        //
        // transform.forward = Vector3.Lerp(transform.forward, transform.up, Time.deltaTime);
        // _rb.velocity = transform.up * speed;
    }
    
}
