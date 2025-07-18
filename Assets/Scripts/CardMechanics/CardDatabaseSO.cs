using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Data/Card Database")]
public class CardDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private List<CardDataSO> cards = new List<CardDataSO>();

    private Dictionary<int, CardDataSO> cardLookup;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null && cards[i].ID != i)
            {
                if (cards[i] != null && cards[i].ID != i)
                {
                    cards[i].SetID(i);
                }
            }
        }
    }

    private void BuildLookupDictionary()
    {
        cardLookup = new Dictionary<int, CardDataSO>();

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                if (cardLookup.ContainsKey(cards[i].ID))
                {
                    Debug.LogWarning($"Duplicate card ID {cards[i].ID} found");
                }
                else
                {
                    cardLookup[cards[i].ID] = cards[i];
                }
            }
        }
    }

    /// <summary>
    /// Get a card by its unique ID
    /// </summary>
    /// <param name="id">The card ID to search for</param>
    /// <returns>The CardDataSO if found, null otherwise</returns>
    public CardDataSO GetCardByID(int id)
    {
        EnsureLookupDictionaryExists();
        cardLookup.TryGetValue(id, out CardDataSO card);
        return card;
    }

    public CardDataSO GetCardByName(string cardName)
    {
        if (string.IsNullOrEmpty(cardName)) return null;

        foreach (var card in cards)
        {
            if (card != null && card.name == cardName)
            {
                return card;
            }
        }
        return null;
    }

    /// <summary>
    /// Get all cards that have a specific type of effect
    /// </summary>
    /// <typeparam name="T">The effect type to search for</typeparam>
    /// <returns>List of cards containing the specified effect type</returns>
    public List<CardDataSO> GetCardsByEffectType<T>() where T : Effect
    {
        List<CardDataSO> result = new List<CardDataSO>();

        foreach (var card in cards)
        {
            if (card == null) continue;

            // Check manual target effect
            if (card.ManualTargetEffect is T)
            {
                result.Add(card);
                continue;
            }

            // Check other effects
            foreach (var autoEffect in card.OtherEffects)
            {
                if (autoEffect?.Effect is T)
                {
                    result.Add(card);
                    break; // Don't add the same card multiple times
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Get all cards that have any effect of the specified types
    /// </summary>
    /// <param name="effectTypes">Array of effect types to search for</param>
    /// <returns>List of cards containing any of the specified effect types</returns>
    public List<CardDataSO> GetCardsByEffectTypes(params Type[] effectTypes)
    {
        List<CardDataSO> result = new List<CardDataSO>();

        foreach (var card in cards)
        {
            if (card == null) continue;

            bool hasMatchingEffect = false;

            // Check manual target effect
            if (card.ManualTargetEffect != null)
            {
                Type manualEffectType = card.ManualTargetEffect.GetType();
                foreach (var searchType in effectTypes)
                {
                    if (searchType.IsAssignableFrom(manualEffectType))
                    {
                        hasMatchingEffect = true;
                        break;
                    }
                }
            }

            // Check other effects if not already found
            if (!hasMatchingEffect)
            {
                foreach (var autoEffect in card.OtherEffects)
                {
                    if (autoEffect?.Effect != null)
                    {
                        Type effectType = autoEffect.Effect.GetType();
                        foreach (var searchType in effectTypes)
                        {
                            if (searchType.IsAssignableFrom(effectType))
                            {
                                hasMatchingEffect = true;
                                break;
                            }
                        }
                        if (hasMatchingEffect) break;
                    }
                }
            }

            if (hasMatchingEffect)
            {
                result.Add(card);
            }
        }

        return result;
    }

    /// <summary>
    /// Get a random card from the database
    /// </summary>
    /// <returns>A random CardDataSO, or null if database is empty</returns>
    public CardDataSO GetRandomCard()
    {
        if (cards.Count == 0) return null;

        // Filter out null cards
        var validCards = cards.Where(card => card != null).ToList();
        if (validCards.Count == 0) return null;

        int randomIndex = Random.Range(0, validCards.Count);
        return validCards[randomIndex];
    }

    /// <summary>
    /// Get multiple random cards without duplicates
    /// </summary>
    /// <param name="count">Number of random cards to get</param>
    /// <returns>List of random cards</returns>
    public List<CardDataSO> GetRandomCardsNoDuplicates(int count)
    {
        var validCards = cards.Where(card => card != null).ToList();
        if (validCards.Count == 0) return new List<CardDataSO>();

        // Clamp count to available cards
        count = Mathf.Min(count, validCards.Count);

        List<CardDataSO> result = new List<CardDataSO>();
        List<CardDataSO> remainingCards = new List<CardDataSO>(validCards);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, remainingCards.Count);
            result.Add(remainingCards[randomIndex]);
            remainingCards.RemoveAt(randomIndex);
        }

        return result;
    }

    public List<CardDataSO> GetRandomCardsWithDuplicates(int count)
    {
        var validCards = cards.Where(card => card != null).ToList();
        if (validCards.Count == 0) return new List<CardDataSO>();

        List<CardDataSO> result = new List<CardDataSO>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, validCards.Count);
            result.Add(validCards[randomIndex]);
        }

        return result;
    }

    /// <summary>
    /// Get all cards in the database
    /// </summary>
    /// <returns>List of all cards (excluding null entries)</returns>
    public List<CardDataSO> GetAllCards()
    {
        return cards.Where(card => card != null).ToList();
    }

    /// <summary>
    /// Get the total number of cards in the database
    /// </summary>
    /// <returns>Number of valid cards</returns>
    public int GetCardCount()
    {
        return cards.Count(card => card != null);
    }

    /// <summary>
    /// Check if a card with the given ID exists in the database
    /// </summary>
    /// <param name="id">The card ID to check</param>
    /// <returns>True if card exists, false otherwise</returns>
    public bool HasCard(int id)
    {
        EnsureLookupDictionaryExists();
        return cardLookup.ContainsKey(id);
    }

    /// <summary>
    /// Ensures the lookup dictionary exists, rebuilding it if necessary
    /// </summary>
    private void EnsureLookupDictionaryExists()
    {
        if (cardLookup == null)
        {
            BuildLookupDictionary();
        }
    }

    public void OnBeforeSerialize()
    {
        // Update IDs before serialization to ensure consistency
        UpdateID();
    }

    public void OnAfterDeserialize()
    {
        // Ensure IDs are correct after deserialization
        UpdateID();
        // Rebuild the lookup dictionary after Unity deserializes the ScriptableObject
        BuildLookupDictionary();
    }
}