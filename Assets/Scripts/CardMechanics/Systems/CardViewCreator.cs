using DG.Tweening;
using UnityEngine;

public class CardViewCreator : MonoBehaviour
{
    [SerializeField]
    private CardView cardViewPrefab;

    public static CardViewCreator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public CardView CreateCardView(Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        cardView.transform.localScale = Vector3.one;
        cardView.transform.DOScale(Vector3.one, 0.15f);
        return cardView;
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
