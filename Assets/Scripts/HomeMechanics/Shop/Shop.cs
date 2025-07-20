using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private Movement playerDeck;
    [SerializeField] private HeroDataSO playerCash;
    [SerializeField] private Button buyButton;
    [SerializeField] private ShopDealer shopDealer;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI cartText;
    public Sprite buyButtonGood;
    public Sprite cancelBuy;
    public Sprite buyButtonBad;
    public static Shop Instance;
    private float fullPrice = 0f;
    private List<ShopCard> shopContent = new();
    public List<ShopCard> shopCart = new();
    private const int SHOP_LIMIT = 10;
    void Awake()
    {
        ActionSystem.SubscribeReaction<AddCardToCartEvent>(AddCardToCart, ReactionTiming.POST);
        if (Instance == null)
        {
            FIllWithRandomCards();
            EvaluateCards();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        fullPrice = 0;
        moneyText.text = "Your money: " + playerCash.Money;
        cartText.text = "Cart:  " + fullPrice;
        foreach (var card in shopCart)
        {
            card.inCart = false;
        }
        shopCart.Clear();
        EvaluateCards();
    }
    public void FIllWithRandomCards()
    {
        for (int i = shopContent.Count; i < SHOP_LIMIT; i++)
        {
            ShopCard card = new ShopCard(CardManagementSystem.Instance.GetRandomCard());
            shopContent.Add(card);
            ShopCardViewCreator.Instance.CreateShopCardView(shopContent[i], new Vector3(0, 0, 0), quaternion.identity);
        }

    }
    public void AddCardToCart(AddCardToCartEvent cardToCartEvent)
    {
        if (!cardToCartEvent.cardToCart.inCart)
        {
            cardToCartEvent.cardToCart.Button.image.sprite = cancelBuy;
            cardToCartEvent.cardToCart.inCart = true;
            fullPrice += cardToCartEvent.cardToCart.Price;
            shopCart.Add(cardToCartEvent.cardToCart);
        }
        else if (cardToCartEvent.cardToCart.inCart)
        {
            cardToCartEvent.cardToCart.Button.image.sprite = buyButtonGood;
            fullPrice -= cardToCartEvent.cardToCart.Price;
            cardToCartEvent.cardToCart.inCart = false;
            shopCart.Remove(cardToCartEvent.cardToCart);
        }
        cartText.text = "Cart:  " + fullPrice;
        EvaluateCards();
    }
    public void BuyItems()
    {

        if (HasEnoughCash(playerCash.Money))
        {
            playerCash.Money -= fullPrice;
            fullPrice = 0;
            shopDealer.gameObject.SetActive(true);
            shopDealer.cardsToGive.AddRange(shopCart);
            DestroyCartCards();
            shopCart.Clear();
        }
        moneyText.text = "Your money: " + playerCash.Money;
        cartText.text = "Cart:  " + fullPrice;
        EvaluateCards();
    }
    public void DestroyCartCards()
    {
        ShopCardView[] allViews = FindObjectsByType<ShopCardView>(FindObjectsSortMode.None);

        foreach (ShopCardView view in allViews)
        {
            if (shopCart.Contains(view.Card))
            {
                view.DestroyCardView();
            }
        }
    }
    public bool HasEnoughCash(float money)
    {
        return money >= fullPrice;
    }
    public void EvaluateCards()
    {
        if (HasEnoughCash(playerCash.Money) && fullPrice != 0 && shopDealer.cardsToGive.Count == 0)
        {
            buyButton.image.sprite = buyButtonGood;
            buyButton.enabled = true;
        }
        else
        {
            buyButton.image.sprite = buyButtonBad;
            buyButton.enabled = false;
        }
        if (shopCart.Count == 0)
        {
            buyButton.image.sprite = buyButtonBad;
            buyButton.enabled = false;
        }
        else
        {
            buyButton.image.sprite = buyButtonGood;
            buyButton.enabled = true;
        }
        foreach (var card in shopContent)
        {
            if (card.Button != null)
            {
                if (shopDealer.cardsToGive.Count != 0 || playerCash.Money == 0)
                {
                    card.Button.image.sprite = buyButtonBad;
                    card.Button.enabled = false;
                }
                else if (shopDealer.cardsToGive.Count == 0 && !card.inCart)
                {
                    card.Button.image.sprite = buyButtonGood;
                    card.Button.enabled = true;
                }
                if (card.Price > playerCash.Money)
                {
                    card.Button.image.sprite = buyButtonBad;
                    card.Button.enabled = false;
                }
                if (card.Price <= playerCash.Money && !card.inCart && shopDealer.cardsToGive.Count == 0)
                {
                    card.Button.image.sprite = buyButtonGood;
                    card.Button.enabled = true;
                }
                if (card.Price > (playerCash.Money - fullPrice) && !card.inCart)
                {
                    card.Button.image.sprite = buyButtonBad;
                    card.Button.enabled = false;
                }
            }
        }
    }
    void OnDisable()
    {
        EvaluateCards();
    }

}


