using NUnit.Framework;
using UnityEngine;

public class AddCardToCartEvent : GameAction
{
    public ShopCard cardToCart{ get; private set; }
    public AddCardToCartEvent(ShopCard card)
    {
        cardToCart = card;
    }

}
