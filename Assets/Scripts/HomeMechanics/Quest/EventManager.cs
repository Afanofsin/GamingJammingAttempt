using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private IEnumerator CardEventPerformer(CollectCardEvent collectCardEvent)
    {
        yield return null;
    }
    public void OnCLick()
    {
        CollectCardEvent collectCardEvent = new();
        ActionSystem.Instance.Perform(collectCardEvent);
    }
    public void AddCardToCart(ShopCard cardView)
    {
        AddCardToCartEvent addCardToCartEvent = new(cardView);
        ActionSystem.Instance.Perform(addCardToCartEvent);
    }
    public void AddCardToDeckBuilder()
    {
        AddCardToDeckBuilder addCardToCartEvent = new();
        ActionSystem.Instance.Perform(addCardToCartEvent);
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
