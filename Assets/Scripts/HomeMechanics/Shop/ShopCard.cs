using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ShopCard
{
    public bool inCart = false;
    public string Title => data.name;
    public Sprite Image => data.Image;
    public Button Button { get;  set; }
    public int Mana { get; private set; }
    public string Description { get; private set; }
    public float Price { get; private set; }
    private readonly CardDataSO data;
    public ShopCard(CardDataSO cardData)
    {
        data = cardData;
        Mana = cardData.ManaCost;
        Description = cardData.Description;
        Price = cardData.CardPrice;
    }
}
