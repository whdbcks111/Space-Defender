using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaserShopItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI priceUI;
    public Button button;
    public RectTransform rectTransform;
    [HideInInspector] public Laser laser;
}
