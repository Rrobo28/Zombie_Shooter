using UnityEngine;
using static Gun;

public class PlayerShooting : MonoBehaviour
{
    Player PlayerScript;

    bool ShootHeld;
    public Gun CurrentGun;
  
    bool Reloading;
    bool ThrowingGrenade;

    public GameObject Grenade;
    public Transform Hand;

    GameObject newGranade;

    void Start()
    {
        PlayerScript = GetComponent<Player>();
    }
    // Update is called once per frame
    public void StartGrenadeThrow()
    {
        if (!ThrowingGrenade)
        {
            PlayerScript.PlayerMovement.CanMove = false;
            PlayerScript.PlayerAnimations.SetSpeed(0);
            CreateGrenade();
        }
    }



    void CreateGrenade()
    {
        ThrowingGrenade = true;

        PlayerScript.PlayerAnimations.SetWeaponType(10);

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
        PlayerScript.PlayerMovement.CanMove = true;
        ThrowingGrenade = false;

        newGranade = null;

        PlayerScript.PlayerAnimations.SetWeaponType(0);

    }


    public void ShootPressed()
    {
        if (ThrowingGrenade || !CurrentGun) return;

        StartShoot();
    }
    public void ShootReleased()
    {
        StopShoot();
    }


    void StartShoot()
    {
        ShootHeld = true;

        FireRate RateOfFire = CurrentGun.GetComponent<Gun>().fireRate;

        PlayerScript.PlayerAnimations.SetFullyAuto(RateOfFire == FireRate.Auto ? true:false);

        if (CurrentGun.MagSize > 0)
        { 
            PlayerScript.PlayerAnimations.SetBodyHorizontal(0.7f);
            PlayerScript.PlayerAnimations.SetHeadHorizontal(-1f);
        }

        if (!Reloading)
        {
            PlayerScript.PlayerAnimations.SetShoot(true);
        }
        else
        {
            PlayerScript.PlayerAnimations.SetShoot(false);
        }
    }
    void StopShoot()
    {
        if (CurrentGun && CurrentGun.MuzzleFlash.isPlaying)
        {
            CurrentGun.MuzzleFlash.Stop();
        }

        ShootHeld = false;

        PlayerScript.PlayerAnimations.SetBodyHorizontal(0);
        PlayerScript.PlayerAnimations.SetHeadHorizontal(0);

        PlayerScript.PlayerAnimations.SetShoot(false);

        PlayerScript.PlayerAnimations.SetFullyAuto(false);
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

        PlayerScript.PlayerHUD.UpdateMagAmmoText(CurrentGun.MagSize);

    }

    void Reload()
    {
        Reloading = true;
        int totalAmmo = 0;
        if(CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Rifle)
        {
            totalAmmo = PlayerScript.PlayerInventory.RifleAmmo;
        }
        else if (CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Pistol)
        {
            totalAmmo = PlayerScript.PlayerInventory.PistolAmmo;
        }

        if (totalAmmo <= 0)
        {
            StopShoot(); 
            return;
        }
        PlayerScript.PlayerAnimations.SetReload(true);
       
    }


    public  void FinishReloading()
    {
        if (CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Rifle)
        {
            if (PlayerScript.PlayerInventory.RifleAmmo >= CurrentGun.TotalMagSize)
            {
                PlayerScript.PlayerInventory.RifleAmmo -= CurrentGun.TotalMagSize;
                CurrentGun.MagSize = CurrentGun.TotalMagSize;

            }
            else
            {
                CurrentGun.MagSize = PlayerScript.PlayerInventory.RifleAmmo;
                PlayerScript.PlayerInventory.RifleAmmo = 0;
            }
            PlayerScript.PlayerHUD.UpdateAmmoText(CurrentGun.MagSize, PlayerScript.PlayerInventory.RifleAmmo);
        }
        else if (CurrentGun.GetComponent<Interact>().type == Interact.ItemType.Pistol)
        {
            if (PlayerScript.PlayerInventory.PistolAmmo >= CurrentGun.TotalMagSize)
            {
                PlayerScript.PlayerInventory.PistolAmmo -= CurrentGun.TotalMagSize;
                CurrentGun.MagSize = CurrentGun.TotalMagSize;

            }
            else
            {
                CurrentGun.MagSize = PlayerScript.PlayerInventory.PistolAmmo;
                PlayerScript.PlayerInventory.PistolAmmo = 0;
            }
            PlayerScript.PlayerHUD.UpdateAmmoText(CurrentGun.MagSize, PlayerScript.PlayerInventory.PistolAmmo);
        }


        PlayerScript.PlayerAnimations.SetReload(false);
        Reloading = false;
    }
}
