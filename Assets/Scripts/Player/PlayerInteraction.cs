using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   public List<GameObject> CurrentlyInteractingWith = new List<GameObject>();

    public Transform RifleSocket;
    public Transform PistolSocket;
    public Transform MeleeSocket;

    Player PlayerScript;

    private void Start()
    {
        PlayerScript = GetComponent<Player>();
    }

    public void AddObject(GameObject Object)
    {
        CurrentlyInteractingWith.Add(Object);
    }
    public void RemoveObject(GameObject Object)
    {
        CurrentlyInteractingWith.Remove(Object);
    }

    public void StartInteraction()
    {
        if (CurrentlyInteractingWith.Count > 0)
        {
            PickUp(CurrentlyInteractingWith[0]);
        }
    }

    void PickUp(GameObject Object)
    {
        Interact interact = Object.GetComponent<Interact>();

        if (interact.type == Interact.ItemType.Rifle)
        {
            PlayerScript.PlayerInventory.PickupPrimary(Object);

            Object.transform.parent = RifleSocket;

            if (PlayerScript.PlayerShooting.CurrentGun == null)
            {
                PlayerScript.PlayerInventory.SwapToPrimary();
            }
            else
            {
                Object.SetActive(false);
            }
        }
        else if (interact.type == Interact.ItemType.Pistol)
        {
            PlayerScript.PlayerInventory.PickupSecondary(Object);

            Object.transform.parent = PistolSocket;

            if (PlayerScript.PlayerShooting.CurrentGun == null)
            {
                PlayerScript.PlayerInventory.SwapToSecondary();
            }
            else
            {
                Object.SetActive(false);
            }
        }
        else if (interact.type == Interact.ItemType.Melee)
        {
            PlayerScript.PlayerInventory.PickupMelee(Object);

            Object.transform.parent = MeleeSocket;

            if (PlayerScript.PlayerShooting.CurrentGun == null && GetComponent<PlayerMelee>().weapon == null)
            {
                PlayerScript.PlayerInventory.SwapToMelee();
            }
            else
            {
                Object.SetActive(false);
            }
        }
        else if (interact.type == Interact.ItemType.Consumable)
        {
            Consumable consumable = interact.GetComponent<Consumable>();

            if (consumable.type == Consumable.ConsumableType.RifleAmmo)
            {
                PlayerScript.PlayerInventory.RifleAmmo += consumable.AmmoQuantity;

                if (PlayerScript.PlayerShooting.CurrentGun != null && PlayerScript.PlayerShooting.CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Rifle)
                {
                    PlayerScript.PlayerHUD.UpdateTotalAmmoText(PlayerScript.PlayerInventory.RifleAmmo);
                }

            }
            else if (consumable.type == Consumable.ConsumableType.PistolAmmo)
            {
                PlayerScript.PlayerInventory.PistolAmmo += consumable.AmmoQuantity;
                if (PlayerScript.PlayerShooting.CurrentGun != null && PlayerScript.PlayerShooting.CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Pistol)
                {
                    PlayerScript.PlayerHUD.UpdateTotalAmmoText(PlayerScript.PlayerInventory.PistolAmmo);
                }
            }
            Destroy(interact.gameObject);
        }

        Object.transform.localPosition = Vector3.zero;
        Object.transform.localRotation = Quaternion.identity;

        interact.DissableInteraction();

        RemoveObject(Object);

      
    }

}
