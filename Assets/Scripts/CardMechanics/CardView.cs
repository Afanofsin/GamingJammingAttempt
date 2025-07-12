using UnityEngine;
using TMPro;
using UnityEditor;

public class CardView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _manaText;
    [SerializeField]
    private TMP_Text _cardNameText;
    [SerializeField]
    private TMP_Text _descriptionText;
    [SerializeField]
    private SpriteRenderer _cardSprite;
    [SerializeField]
    private GameObject _wrapper;

    public Card Card {  get; private set; }

    public void Setup(Card card)
    {
        Card = card;
        _cardNameText.text = card.Title;
        _descriptionText.text = card.Description;
        _manaText.text = card.Mana.ToString();
        _cardSprite.sprite = card.Image;
    }
}
