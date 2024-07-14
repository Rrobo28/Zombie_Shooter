using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    PlayerAnimations thisAnim;

    bool ShootHeld;

    public Gun CurrentGun;

    PlayerInventory inventory;

    bool Reloading;

    bool ThrowingGrenade;

    public GameObject Grenade;

    public Transform Hand;

    GameObject newGranade;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        thisAnim = GetComponent<PlayerAnimations>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G) && !ThrowingGrenade)
        {
            StartThrow();
        }


        if (CurrentGun == null)
        {
            return;
        }


        if(Input.GetMouseButton(0) && !ThrowingGrenade)
        {
            Shoot();
        }
        else
        {
            StopShoot();
        }

      
    }

    void StartThrow()
    {
        ThrowingGrenade = true;

        thisAnim.SetWeaponType(10);

        newGranade = Instantiate(Grenade, Hand.transform.position, Quaternion.identity);

        newGranade.transform.parent = Hand;

    }

    public void GrenadeThrow()
    {
        if (newGranade == null) return;

        newGranade.transform.parent = null;

        Rigidbody body = newGranade.GetComponent<Rigidbody>();
        body.isKinematic = false;
        body.AddForceAtPosition(transform.forward * 10,Hand.transform.position,ForceMode.Impulse);
    }


    public void EndGrenadeThrow()
    {
        ThrowingGrenade = false;

        newGranade = null;

        thisAnim.SetWeaponType(0);
    }


    void Shoot()
    {
        ShootHeld = true;

       
        Interact WeaponType = CurrentGun.GetComponent<Interact>();


        if(WeaponType.type == Interact.ItemType.Pistol)
        {
        }

       
        thisAnim.SetBodyHorizontal(0.7f);
        thisAnim.SetHeadHorizontal(-1f);

        thisAnim.SetShoot(true);
        thisAnim.SetFullyAuto(true);
    }
    void StopShoot()
    {

        if (CurrentGun.MuzzleFlash.isPlaying)
        {
            Debug.Log("Stop Particle");
            CurrentGun.MuzzleFlash.Stop();
        }

        ShootHeld = false;
        thisAnim.SetBodyHorizontal(0);
        thisAnim.SetHeadHorizontal(0);

        thisAnim.SetShoot(false);
       
    }

    public void ShootBullet()
    {
        if (CurrentGun == null || !ShootHeld)
        {
            return;
        }

        if (CurrentGun.MagSize <= 0) 
        {
            if(!Reloading)
            {
                Reload();
            }
            
            return;
        
        }

        CurrentGun.ShootBullet();

        CurrentGun.MagSize--;

        GetComponent<PlayerHUD>().UpdateMagAmmoText(CurrentGun.MagSize);

    }

    void Reload()
    {
        int totalAmmo = 0;
        if(CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Rifle)
        {
            totalAmmo = inventory.RifleAmmo;
        }
        else if (CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Pistol)
        {
            totalAmmo = inventory.PistolAmmo;
        }

        if (totalAmmo <= 0) return; 

        Reloading = true;

        thisAnim.SetReload(true);
       
    }


    public  void FinishReloading()
    {
        if (CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Rifle)
        {
            if (inventory.RifleAmmo >= CurrentGun.TotalMagSize)
            {
                inventory.RifleAmmo -= CurrentGun.TotalMagSize;
                CurrentGun.MagSize = CurrentGun.TotalMagSize;

            }
            else
            {
                CurrentGun.MagSize = inventory.RifleAmmo;
                inventory.RifleAmmo = 0;
            }
            GetComponent<PlayerHUD>().UpdateAmmoText(CurrentGun.MagSize, inventory.RifleAmmo);
        }
        else if (CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Pistol)
        {
            if (inventory.PistolAmmo >= CurrentGun.TotalMagSize)
            {
                inventory.PistolAmmo -= CurrentGun.TotalMagSize;
                CurrentGun.MagSize = CurrentGun.TotalMagSize;

            }
            else
            {
                CurrentGun.MagSize = inventory.PistolAmmo;
                inventory.PistolAmmo = 0;
            }
            GetComponent<PlayerHUD>().UpdateAmmoText(CurrentGun.MagSize, inventory.PistolAmmo);
        }

      
        thisAnim.SetReload(false);
        Reloading = false;
    }
}
