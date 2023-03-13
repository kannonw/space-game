using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] float minScale, maxScale, tumble;
        private Transform _myT;

        // public Asteroid(float minScale, float maxScale, float tumble)
        // {
        //     _minScale = minScale;
        //     _maxScale = maxScale;
        //     _tumble = tumble;
        // }
        
        void Awake()
        {
            _myT = transform;
        }

        void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
            _myT.localScale = Vector3.one * Random.Range(minScale, maxScale);
        }
    }
}
