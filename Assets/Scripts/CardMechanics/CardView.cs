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
    [SerializeField]
    private LayerMask _layerMask;
    private Camera _cam;

    public Card Card {  get; private set; }
    private Vector3 dragStartPos;
    private Quaternion dragStartRotation;

    private void Awake()
    {
        _cam = Camera.main;
    }

    public void Setup(Card card)
    {
        Card = card;
        _cardNameText.text = card.Title;
        _descriptionText.text = card.Description;
        _manaText.text = card.Mana.ToString();
        _cardSprite.sprite = card.Image;
    }

    private void OnMouseEnter()
    {
        if (!InteractionsSystem.Instance.PlayerCanHover()) return;
        _wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -5, 0);
        CardViewHover.Instance.ShowAnim(Card, transform.position, pos, transform.rotation);
        //CardViewHover.Instance.Show(Card, pos);
    }

    private void OnMouseExit()
    {
        if (!InteractionsSystem.Instance.PlayerCanHover()) return;
        CardViewHover.Instance.Hide();
        _wrapper.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!InteractionsSystem.Instance.PlayerCanInteract()) return;
        if(Card.ManualTargetEffect != null)
        {
            ManualTargetSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            InteractionsSystem.Instance.PlayerIsDraggin = true;
            _wrapper.SetActive(true);
            CardViewHover.Instance.Hide();
            dragStartPos = transform.position;
            dragStartRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = MouseUtils.GetMousePositionInWorldSpace(transform.position.z);
        }
    }

    private void OnMouseDrag()
    {
        if (!InteractionsSystem.Instance.PlayerCanInteract()) return;
        if (Card.ManualTargetEffect != null) return;
        transform.position = MouseUtils.GetMousePositionInWorldSpace(transform.position.z);
    }

    private void OnMouseUp()
    {
        if (!InteractionsSystem.Instance.PlayerCanInteract()) return;
        if (Card.ManualTargetEffect != null)
        {
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtils.GetMousePositionInWorldSpace(-1));
            if(target != null && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
                InteractionsSystem.Instance.PlayerIsDraggin = false;
            }
        }
        else
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.forward, 100f, _layerMask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != this.gameObject &&
                    ManaSystem.Instance.HasEnoughMana(Card.Mana))
                {
                    PlayCardGA playCardGA = new(Card);
                    ActionSystem.Instance.Perform(playCardGA);
                    InteractionsSystem.Instance.PlayerIsDraggin = false;
                    return;
                }
            }
            transform.position = dragStartPos;
            transform.rotation = dragStartRotation;
            InteractionsSystem.Instance.PlayerIsDraggin = false;
        }

    }
}
