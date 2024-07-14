using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player PlayerScript;
    private PlayerInput PlayerInput;

    private PlayerInputActions PlayerInputActions;

    InputAction Move;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();

        PlayerScript = GetComponent<Player>();
        PlayerInput = GetComponent<PlayerInput>();

        
    }

    private void OnEnable()
    {
        Move = PlayerInputActions.Gameplay.Move;
        Move.Enable();
    }


    public Vector3 GetMovementInputVector()
    {
        return new Vector3 (Move.ReadValue<Vector2>().x,0, Move.ReadValue<Vector2>().y);
    }





    private void OnDisable()
    {
        Move.Disable();
    }
}
