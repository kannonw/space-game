using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalAttraction : MonoBehaviour
{
    public Rigidbody rb;
    public static List<GravitationalAttraction> Attractors;
    private readonly float _g = 6.67408f * 10; // * Mathf.Pow(10, -1);
    public Vector3 GravityForce { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<GravitationalAttraction>();
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
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

