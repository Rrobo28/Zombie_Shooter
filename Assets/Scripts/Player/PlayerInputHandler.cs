using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    [Header("Controls")]
    public bool ToggleSprint = false;


    private Player PlayerScript;
    private PlayerInput PlayerInput;

    private PlayerInputActions PlayerInputActions;

    InputAction Move;
    InputAction Sprint;

    InputAction Interact;

    InputAction Grenade;
    InputAction Attack;
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

        Sprint = PlayerInputActions.Gameplay.Sprint;
        Sprint.performed += SprintPressed;
        Sprint.canceled += SprintReleased;
        Sprint.Enable();

        Interact = PlayerInputActions.Gameplay.Interact;
        Interact.performed += InteractPressed;
        Interact.Enable();

        Attack = PlayerInputActions.Gameplay.Attack;
        Attack.performed += AttackPressed;
        Attack.canceled += AttackReleased;
        Attack.Enable();

        Grenade = PlayerInputActions.Gameplay.Grenade;
        Grenade.performed += GrenadePressed;
        Grenade.canceled += GrenadeReleased;
        Grenade.Enable();
    }

    public void AttackPressed(InputAction.CallbackContext context)
    {
        PlayerScript.PlayerShooting.ShootPressed();
    }
    public void AttackReleased(InputAction.CallbackContext context)
    {
        PlayerScript.PlayerShooting.ShootReleased();
    }

    public void GrenadePressed(InputAction.CallbackContext context)
    {
        PlayerScript.PlayerShooting.StartGrenadeThrow();
    }
    public void GrenadeReleased(InputAction.CallbackContext context)
    {

    }



    public void InteractPressed(InputAction.CallbackContext context)
    {
        PlayerScript.PlayerInteraction.StartInteraction();
    }

    public void SprintPressed(InputAction.CallbackContext context)
    {
        if (ToggleSprint)
        {
            PlayerScript.PlayerMovement.ToggleSprint();
        }
        else
        {
            PlayerScript.PlayerMovement.StartSprint();
        }
        
    }
    public void SprintReleased(InputAction.CallbackContext context)
    {
        if (!ToggleSprint)
        {
            PlayerScript.PlayerMovement.StopSprint();
        }
    }


    public Vector3 GetMovementInputVector()
    {
        return new Vector3 (Move.ReadValue<Vector2>().x,0, Move.ReadValue<Vector2>().y);
    }





    private void OnDisable()
    {
        Move.Disable();
        Sprint.Disable();
        Interact.Disable();
    }
}
