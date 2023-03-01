using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUi : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI text;
    
    void Update()
    {
        text.text = Spaceship.kmp.ToString("F0");
    }
}
