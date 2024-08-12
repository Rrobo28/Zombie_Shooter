using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkinnedMeshRenderer PlayerMesh;

    [HideInInspector]
    public Transform PlayerTransform;

    [HideInInspector]
    public PlayerMovement PlayerMovement;
    [HideInInspector]
    public PlayerAnimations PlayerAnimations;
    [HideInInspector]
    public PlayerInputHandler PlayerInputHandler;
    [HideInInspector]
    public PlayerInventory PlayerInventory;
    [HideInInspector]
    public PlayerShooting PlayerShooting;
    [HideInInspector]
    public PlayerHUD PlayerHUD;
    [HideInInspector]
    public PlayerInteraction PlayerInteraction;

    private void Awake()
    {
       
        PlayerTransform = transform;

        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAnimations =  GetComponent<PlayerAnimations>();
        PlayerInputHandler =  GetComponent<PlayerInputHandler>();
        PlayerInventory = GetComponent<PlayerInventory>();
        PlayerShooting = GetComponent<PlayerShooting>();
        PlayerHUD = GetComponent<PlayerHUD>();
        PlayerInteraction = GetComponent<PlayerInteraction>();
    }
}
