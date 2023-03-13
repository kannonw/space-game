using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomScale : MonoBehaviour
{
    [SerializeField] float minScale, maxScale;
    private Transform _myT;
    void Awake()
    {
        _myT = transform;
    }

    void Start()
    {
        _myT.localScale = Vector3.one * Random.Range(minScale, maxScale);
    }
}
