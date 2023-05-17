using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{
    public Camera cam;
    public float initView;
    private float _velocity;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        initView = cam.fieldOfView;
    }

    private void Update()
    {
        _velocity = Spaceship.Kpm;
        
        if (_velocity > 0)
        {
            if (_velocity <= 10)
                Follow(_velocity * 2f + initView);
            return;
        }
        
        Follow(initView);
    }

    // void FixedUpdate()
    // {
    //     
    //     
    //     // if (Input.GetAxis("Forward") <= 0 && cam.fieldOfView > initView)
    //     //     Follow(initView);
    //     //
    //     // if (Input.GetAxis("Forward") > 0)
    //     //     Follow(initView + 30);
    // }

    void Follow(float view)
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, view, Time.deltaTime * 3f);
    }
}