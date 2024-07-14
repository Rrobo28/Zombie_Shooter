using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   public Gun PrimaryGun;
   public Gun SecondaryGun;

    public MeleeWeapon MeleeWeapon;


    public int RifleAmmo;
    public int PistolAmmo;
    public void PickupPrimary(GameObject Gun)
    {
        if(PrimaryGun != null)
        {
            DropWeapon(PrimaryGun.gameObject);
        }
        PrimaryGun = Gun.GetComponent<Gun>(); 
    }
    public void PickupMelee(GameObject Weapon)
    {
        if (MeleeWeapon != null)
        {
            DropWeapon(MeleeWeapon.gameObject);
        }
        MeleeWeapon = Weapon.GetComponent<MeleeWeapon>();
    }

    public void PickupSecondary(GameObject Gun)
    {
        if (SecondaryGun != null)
        {
            DropWeapon(SecondaryGun.gameObject);
        }
        SecondaryGun = Gun.GetComponent<Gun>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))   
        {
            SwapToPrimary();
        
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapToSecondary();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapToMelee();
        }
    }

   public void SwapToPrimary()
    {
        if (PrimaryGun == null) return;
        if(SecondaryGun != null)
        {
            SecondaryGun.gameObject.SetActive(false);
        }
        if (MeleeWeapon != null)
        {
            MeleeWeapon.gameObject.SetActive(false);
        }
        PrimaryGun.gameObject.SetActive(true);
        GetComponent<PlayerShooting>().CurrentGun = PrimaryGun;
        GetComponent<PlayerMelee>().weapon = null;
        GetComponent<PlayerAnimations>().SetWeaponType(3);

        GetComponent<PlayerHUD>().UpdateAmmoText(PrimaryGun.MagSize, RifleAmmo);

    }
    public void SwapToSecondary()
    {
        if (SecondaryGun == null) return;
        if (PrimaryGun != null)
        {
            PrimaryGun.gameObject.SetActive(false);
        }
        if (MeleeWeapon != null)
        {
            MeleeWeapon.gameObject.SetActive(false);
        }
        SecondaryGun.gameObject.SetActive(true);
        GetComponent<PlayerShooting>().CurrentGun = SecondaryGun;
        GetComponent<PlayerMelee>().weapon = null;
        GetComponent<PlayerAnimations>().SetWeaponType(1);

        GetComponent<PlayerHUD>().UpdateAmmoText(SecondaryGun.MagSize, PistolAmmo);
    }
    public void SwapToMelee()
    {
        if (MeleeWeapon == null) return;


        if (SecondaryGun != null)
        {
            SecondaryGun.gameObject.SetActive(false);
        }
        if (PrimaryGun != null)
        {
            PrimaryGun.gameObject.SetActive(false);
        }
        MeleeWeapon.gameObject.SetActive(true);

        GetComponent<PlayerShooting>().CurrentGun = null;
        GetComponent<PlayerMelee>().weapon = MeleeWeapon;
    }



    void DropWeapon(GameObject Weapon)
    {

    }


}
