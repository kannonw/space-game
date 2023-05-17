using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        _playerInputActions = new();
        _playerInputActions.Player.Enable();
    }

    public float GetMovementStraight()
    {
        return _playerInputActions.Player.Move.ReadValue<float>();
    }
    
    public Vector3 GetMovementRotate()
    {
        return _playerInputActions.Player.Rotate.ReadValue<Vector3>();
    }
}
