using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalAttraction : MonoBehaviour
{
    [SerializeField] public bool beAttracted;
    [SerializeField] private bool notUseInitialVelocity;
    [SerializeField] private Vector3 customInitialVelocity;
    
    public Rigidbody rb { get; private set; }

    public static List<GravitationalAttraction> Attractors { get; private set; }
    public static readonly float G = 6.67408f * Mathf.Pow(10, 1);

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<GravitationalAttraction>();
        
        rb = GetComponent<Rigidbody>();
        Attractors.Add(this);
    }
    
    
    private void OnDisable()
    {
        Attractors.Remove(this);
    }
    

    private void Start()
    {
        if (notUseInitialVelocity) return;

        if (customInitialVelocity != Vector3.zero)
        {
            rb.velocity = customInitialVelocity;
            return;
        }
        
        float strongestBodyForce = 0f;
        float strongestBodyMass = 0f;
        float radius = 0f;

        foreach (GravitationalAttraction obj in Attractors)
        {
            if (obj == this) continue;
            
            Vector3 direction = rb.position - obj.rb.position;
            float force = obj.rb.mass * rb.mass / (direction.magnitude * direction.magnitude);

            if (force > strongestBodyForce)
            {
                strongestBodyForce = force;
                strongestBodyMass = obj.rb.mass;
                radius = direction.magnitude;
            }
        }
        // Debug.Log("-");
        // Debug.Log(strongestBodyMass);
        // Debug.Log(strongestBodyForce);
        // Debug.Log(radius);
        // Debug.Log(new Vector3(Mathf.Sqrt(G * (strongestBodyMass + rb.mass) / radius), 0f, 0f));
        
        rb.velocity = new Vector3(Mathf.Sqrt(G * (strongestBodyMass + rb.mass) / radius), 0f, 0f);
    }


    private void FixedUpdate()
    {
        foreach (GravitationalAttraction obj in Attractors)
        {
            if (obj != this && obj.beAttracted)
                Attract(obj);
        }
    }
    

    private void Attract(GravitationalAttraction objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;
        Vector3 direction = rb.position - rbToAttract.position;
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / (direction.magnitude * direction.magnitude);
        Vector3 gravityForce = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(gravityForce);
    }
}