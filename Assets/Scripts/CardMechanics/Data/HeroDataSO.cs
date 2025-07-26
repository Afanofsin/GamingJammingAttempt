using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Hero")]
public class HeroDataSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public float Money { get; set; }
    [field: SerializeField]
    public int Health { get; private set; }
    [field: SerializeField]
    public int Morale { get; private set; }
    [field: SerializeField]
    public RuntimeAnimatorController Controller { get; private set; }
    [field: SerializeField]
    public List<CardDataSO> Deck { get; private set; }
    [field: SerializeField]
    public List<CardDataSO> InventoryDeck { get; private set; }
    public void AddCardToDeck(CardDataSO cardDataSo)
    {
        Deck.Add(cardDataSo);
    }
    public void RebuildBattleDeck(List<Card> cards)
    {
        Deck.Clear();
        foreach (Card card in cards)
        {
            Deck.Add(card.data);
        }
    }

    public void RebuildInventoryDeck(List<Card> cards)
    {
        InventoryDeck.Clear();
        foreach (Card card in cards)
        {
            InventoryDeck.Add(card.data);
        }
    }

    public void InitializeBattleDeck(List<CardDataSO> cards)
    {
        Deck = new List<CardDataSO>(cards);
    }

    public void InitializeInventoryDeck(List<CardDataSO> cards)
    {
        InventoryDeck = new List<CardDataSO>(cards);
    }

    public void Reset()
    {
        InventoryDeck = new();
        Deck = new();
        Money = 0;
    }
}
