using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{

    public MeleeWeapon weapon;

    bool ButtonDown = false;

    PlayerAnimations animations;

    private void Start()
    {
        animations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (weapon == null) return;

        if(Input.GetMouseButtonDown(0))
        {
            ButtonDown = true;
            Melee();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ButtonDown = false;
            animations.SetMeleeType(10);
        }
    }


    void Melee()
    {
        animations.SetMeleeType(2);
    }
}
