using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHUD : MonoBehaviour
{
    public Canvas HUD;
    public TextMeshProUGUI MagAmmoText;
    public TextMeshProUGUI TotalAmmoText;

    public void UpdateMagAmmoText(int mag)
    {
        MagAmmoText.text = mag.ToString();
    }
    public void UpdateTotalAmmoText(int Total)
    {
        TotalAmmoText.text = Total.ToString();
    }
    public void UpdateAmmoText(int Mag,int Total)
    {
        MagAmmoText.text = Mag.ToString();
        TotalAmmoText.text = Total.ToString();
    }
}
