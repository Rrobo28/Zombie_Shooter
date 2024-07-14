using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;
public class Interact : MonoBehaviour
{
    public enum Rarity { Common, Uncommon, Rare,Epic,Legendary }

    public Rarity rarity;
   

    public enum ItemType { Rifle, Pistol, Melee, Consumable }

    public ItemType type;

    public Outline outline;

    public Canvas Message;

    bool Disabled = false;

    public TextMeshProUGUI MessageText;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        Message.enabled = false;


        if(rarity == Rarity.Uncommon)
        {
           SetColour(Color.white);
        }
        else if (rarity == Rarity.Common)
        {
            SetColour(Color.green);
        }
        else if (rarity == Rarity.Rare)
        {
            SetColour(Color.blue);
        }
        else if (rarity == Rarity.Epic)
        {
            Color32 purple = new Color32(150, 0, 255, 255);
            SetColour(purple);
        }
        else if (rarity == Rarity.Rare)
        {
            SetColour(Color.blue);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Disabled)
        {
            Highlight();
            other.GetComponent<PlayerInteraction>().AddObject(this.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !Disabled)
        {
            Highlight();
            other.GetComponent<PlayerInteraction>().RemoveObject(this.gameObject);
        }
    }

    void Update()
    {
        if (Message.isActiveAndEnabled)
        {
            Message.transform.rotation = Quaternion.LookRotation(Message.transform.position - Camera.main.transform.position);
        }
    }

    void Highlight()
    {
        if(!outline.enabled)
        {
            outline.enabled = true;
            Message.enabled = true;
        }

        else
        {
            outline.enabled = false;
            Message.enabled = false;
        }
    }

    public void DissableInteraction()
    {
        outline.enabled = false;
        Message.enabled = false;

        Disabled = true; 


    }

    void SetColour(Color colour)
    {
        MessageText.color = colour;
        outline.OutlineColor = colour;
    }

}
