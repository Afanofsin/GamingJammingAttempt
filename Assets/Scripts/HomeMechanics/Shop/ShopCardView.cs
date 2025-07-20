using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class ShopCardView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _manaText;
    [SerializeField]
    public TMP_Text _cardNameText;
    [SerializeField]
    private TMP_Text _descriptionText;
    [SerializeField]
    private TMP_Text _priceText;
    [SerializeField]
    private Image _cardImage;
    [SerializeField]
    private GameObject _wrapper;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    public Button _cardButton;
    private Camera _cam;

    public ShopCard Card { get; private set; }

    private void Awake()
    {
        
        _cam = Camera.main;
    }

    public void Setup(ShopCard card)
    {
        Card = card;
        _cardNameText.text = card.Title;
        _descriptionText.text = card.Description;
        _manaText.text = card.Mana.ToString();
        _cardImage.sprite = card.Image;
        _priceText.text = card.Price.ToString();
        card.Button = _cardButton;
        _cardButton = card.Button;

    }

    public void DestroyCardView()
    {
        Destroy(gameObject);
    }

    public void AddCard()
    {
        EventManager.Instance.AddCardToCart(Card);
    }
}