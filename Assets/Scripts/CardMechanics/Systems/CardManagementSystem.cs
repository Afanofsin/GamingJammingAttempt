using JetBrains.Annotations;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManagementSystem : MonoBehaviour
{
    public static CardManagementSystem Instance;
    [SerializeField]
    private CardDatabaseSO _database;
    public CardDataSO GetCardByID(int id)
    {
        return _database.GetCardByID(id);
    }
    public CardDataSO GetCardByName(string name)
    {
        return _database.GetCardByName(name);
    }

    public CardDataSO GetRandomCard()
    {
        return _database.GetRandomCard();
    }

    public List<CardDataSO> GetRandomCardsNoDuplicates(int count)
    {
        return _database.GetRandomCardsNoDuplicates(count);
    }

    public List<CardDataSO> GetRandomCardsWithDuplicates(int count)
    {
        return _database.GetRandomCardsWithDuplicates(count);
    }

    public List<CardDataSO> GetAllCards()
    {
        List<CardDataSO> list = _database.GetAllCards();
        return list;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
