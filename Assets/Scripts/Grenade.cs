using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int Damage;

    public int FuseTimer;

    public ParticleSystem Explosion;
    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(FuseTimer);

        Explode();
    }


    void Explode()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Explosion.Play();


    }

   
}
