using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalAttraction : MonoBehaviour
{
    private Rigidbody rb;
    private const float _g = 6.67408f * 10;
    public static List<GravitationalAttraction> Attractors;
    public Vector3 GravityForce { get; private set; }
    public Vector3 initialVelocity;

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<GravitationalAttraction>();
    }
    private void OnDisable()
    {
        Attractors.Remove(this);
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
        Attractors.Add(this);
    }

    
    void FixedUpdate()
    {
        foreach (GravitationalAttraction obj in Attractors)
        {
            if (obj != this)
                Attract(obj);
        }
    }



    void Attract(GravitationalAttraction objToAttract)
    {
        Rigidbody rbToAttrack = objToAttract.rb;
        Vector3 direction = rb.position - rbToAttrack.position;
        float forceMargnitude = _g * (rb.mass * rbToAttrack.mass) / Mathf.Pow(direction.magnitude, 2);
        GravityForce = direction.normalized * forceMargnitude;

        rbToAttrack.AddForce(GravityForce);
    }
}