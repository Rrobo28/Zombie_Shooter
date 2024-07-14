using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

   public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed_f", speed);
    }

    public void SetWeaponType(int value)
    {
        animator.SetInteger("WeaponType_int", value);
    }
    public void SetMeleeType(int value)
    {
        animator.SetInteger("MeleeType_int", value);
    }

    public void SetBodyVertical(float value)
    {
        animator.SetFloat("Body_Vertical_f", value);
    }
    public void SetBodyHorizontal(float value)
    {
        animator.SetFloat("Body_Horizontal_f", value);
    }
    public void SetHeadHorizontal(float value)
    {
        animator.SetFloat("Head_Horizontal_f", value);
    }
    public void SetHeadVertical(float value)
    {
        animator.SetFloat("Head_Vertical_f", value);
    }
    public void SetFullyAuto(bool value)
    {
        animator.SetBool("FullAuto_b", value);
    }
     public void SetShoot(bool value)
    {
        animator.SetBool("Shoot_b", value);
    }
    public void SetReload(bool value)
    {
        animator.SetBool("Reload_b", value);
    }
   
}
