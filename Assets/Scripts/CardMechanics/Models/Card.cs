using NUnit.Framework;
using UnityEngine;

public class Card
{
    public string Title => data.name;
    public Sprite Image => data.Image;

    public int Mana {  get; private set; }
    public string Description { get; private set; }

    private readonly CardDataSO data;
    public Card(CardDataSO cardData)
    {
        data = cardData;
        Mana = cardData.ManaCost;
        Description = cardData.Description;
    }
}
