using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Gun : MonoBehaviour
{

    public enum FireRate { Auto, SemiAuto, Melee }

    public FireRate fireRate;

    public int MagSize;

    public int TotalMagSize;

    public float SprayAmount;

    public ParticleSystem MuzzleFlash;

    public GameObject Muzzle;

    public ParticleSystem hitParticle;

    void Start()
    {
        MagSize = TotalMagSize;
    }

   public void ShootBullet()
    {
        MuzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(Muzzle.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Instantiate(hitParticle, hit.point, Quaternion.identity);
        }

    }
}
