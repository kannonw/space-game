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

    void Start()
    {
        cam = GetComponent<Camera>();
        initView = cam.fieldOfView;
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Forward") <= 0 && cam.fieldOfView > initView)
            Follow(initView);
        
        if (Input.GetAxis("Forward") > 0)
            Follow(initView + 30);
    }

    void Follow(float view)
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, view, Time.deltaTime);
    }
}