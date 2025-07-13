using DG.Tweening;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class CardViewHover : MonoBehaviour
{
    public static CardViewHover Instance { get; private set; }
    [SerializeField]
    private CardView cardViewHover;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position;
    }

    public void ShowAnim(Card card, Vector3 oldPosition, Vector3 newPosition, Quaternion oldRotation)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.transform.position = oldPosition;
        cardViewHover.transform.rotation = oldRotation;
        cardViewHover.Setup(card);
        cardViewHover.transform.DOMove(newPosition, 0.1f);
        cardViewHover.transform.DORotateQuaternion(Quaternion.identity, 0.1f);
    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }

}
