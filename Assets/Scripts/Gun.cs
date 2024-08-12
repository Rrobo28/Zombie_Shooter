using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Gun : MonoBehaviour
{
    public enum FireRate { Auto, SemiAuto, Melee }

    [Header ("Stats")]
    public FireRate fireRate;
    public int MagSize;
    public int TotalMagSize;
    public float SprayAmount;

    [Header("Effects")]
    public ParticleSystem MuzzleFlash;
    public ParticleSystem hitParticle;

    [Header("Bullet Spawn ")]
    public GameObject Muzzle;

    void Start ()
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
