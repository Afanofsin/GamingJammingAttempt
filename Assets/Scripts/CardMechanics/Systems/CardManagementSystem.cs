using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class CardManagementSystem : MonoBehaviour
{
    public static CardManagementSystem Instance;

    [SerializeField]
    private CardDatabaseSO _database;

    public void GetCardByID(int id)
    {
        _database.GetCardByID(id);
    }

    public void GetCardByName(string name)
    {
        _database.GetCardByName(name);
    }

    public void GetRandomCard()
    {
        _database.GetRandomCard();
    }

    public void GetRandomCardsNoDuplicates(int count)
    {
        _database.GetRandomCardsNoDuplicates(count);
    }

    public void GetRandomCardsWithDuplicates(int count)
    {
        _database.GetRandomCardsWithDuplicates(count);
    }

    public void GetAllCards()
    {
        List<CardDataSO> list = _database.GetAllCards();
        foreach (var card in list)
        {
            Debug.Log(card);
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
