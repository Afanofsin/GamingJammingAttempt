using UnityEngine;
using TMPro;

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
}
