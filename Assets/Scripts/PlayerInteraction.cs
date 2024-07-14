using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   public List<GameObject> CurrentlyInteractingWith = new List<GameObject>();

    public Transform RifleSocket;
    public Transform PistolSocket;
    public Transform MeleeSocket;
    PlayerShooting playerShooting;
    PlayerAnimations playerAnimations;
    PlayerInventory playerInventory;


    private void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void AddObject(GameObject Object)
    {
        CurrentlyInteractingWith.Add(Object);
    }
    public void RemoveObject(GameObject Object)
    {
        CurrentlyInteractingWith.Remove(Object);
    }


    void Update()
    {
        if(Input.GetButtonDown("Interact") && CurrentlyInteractingWith.Count > 0)
        {
            PickUp(CurrentlyInteractingWith[0]);
        }
    }

    void PickUp(GameObject Object)
    {

        Debug.Log("PickUP");

        Interact interact = Object.GetComponent<Interact>();

        if (interact.type == Interact.ItemType.Rifle)
        {
            playerInventory.PickupPrimary(Object);

            Object.transform.parent = RifleSocket;

            if (playerShooting.CurrentGun == null)
            {
                playerInventory.SwapToPrimary();
            }
            else
            {
                Object.SetActive(false);
            }




        }
        else if (interact.type == Interact.ItemType.Pistol)
        {
            playerInventory.PickupSecondary(Object);

            Object.transform.parent = PistolSocket;

            if (playerShooting.CurrentGun == null)
            {
                playerInventory.SwapToSecondary();
            }
            else
            {
                Object.SetActive(false);
            }
        }
        else if (interact.type == Interact.ItemType.Melee)
        {
            playerInventory.PickupMelee(Object);

            Object.transform.parent = MeleeSocket;

            if (playerShooting.CurrentGun == null && GetComponent<PlayerMelee>().weapon == null)
            {
                playerInventory.SwapToMelee();
            }
            else
            {
                Object.SetActive(false);
            }
        }

        Object.transform.localPosition = Vector3.zero;
        Object.transform.localRotation = Quaternion.identity;


        interact.DissableInteraction();

        RemoveObject(Object);

        if (interact.type == Interact.ItemType.Consumable)
        {
            Consumable consumable = interact.GetComponent<Consumable>();

            if(consumable.type == Consumable.ConsumableType.RifleAmmo)
            {
                playerInventory.RifleAmmo += consumable.AmmoQuantity;

                if(playerShooting.CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Rifle)
                {
                    GetComponent<PlayerHUD>().UpdateTotalAmmoText(playerInventory.RifleAmmo);
                }
              
            }
            else if (consumable.type == Consumable.ConsumableType.PistolAmmo)
            {
                playerInventory.PistolAmmo += consumable.AmmoQuantity;
                if (playerShooting.CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Pistol)
                {
                    GetComponent<PlayerHUD>().UpdateTotalAmmoText(playerInventory.PistolAmmo);
                }
            }

            Destroy(interact.gameObject);

        }





      
    }

}
