using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
   public enum ConsumableType { RifleAmmo,PistolAmmo,Health,Grenade,RepairKit}

    public ConsumableType type;

    public int AmmoQuantity;
}
