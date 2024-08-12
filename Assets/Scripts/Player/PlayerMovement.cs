using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]
    private float WalkSpeed = 10;
    [SerializeField]
    private float SprintSpeed = 50;

    [Header("Rotation")]
    [SerializeField]
    private float RotationSpeed = 50;

    public bool CanMove = true;

    private bool Sprinting;
    private float CurrentMoveSpeed;

    private Player PlayerScript;

    private CharacterController CharacterController;
    void Start()
    {
        Sprinting = false;

        CharacterController = GetComponent<CharacterController>();
        PlayerScript = GetComponent<Player>();
    }

    void Update()
    {
        Vector3 InputVector = PlayerScript.PlayerInputHandler.GetMovementInputVector();

        if(InputVector == Vector3.zero)
        {
            if(PlayerScript.PlayerInputHandler.ToggleSprint)
            {
                Sprinting = false;
            }

            CurrentMoveSpeed = 0;
        }
        else
        {
            if (Sprinting)
            {
                CurrentMoveSpeed = SprintSpeed;
            }
            else
            {
                CurrentMoveSpeed = WalkSpeed;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(InputVector),RotationSpeed * Time.deltaTime);
            
        }

        if (CanMove)
        {
            CharacterController.Move(InputVector.normalized * CurrentMoveSpeed * Time.deltaTime);
            PlayerScript.PlayerAnimations.SetSpeed(CurrentMoveSpeed);
        }
     
    }

    public void ToggleSprint()
    {
        Sprinting = !Sprinting;
    }

    
    public void StartSprint()
    {
        Sprinting = true;
    }
    public void StopSprint()
    {
        Sprinting = false;
    }


}
