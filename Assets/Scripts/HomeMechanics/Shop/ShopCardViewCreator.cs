using System.Collections.Generic;
using UnityEngine;

public class ShopCardViewCreator : MonoBehaviour
{
    [SerializeField]
    private ShopCardView cardViewPrefab;
    public ShopCardView deckerViewPrefab;
    public static ShopCardViewCreator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public ShopCardView CreateShopCardView(ShopCard card, Vector3 position, Quaternion rotation)
    {
        ShopCardView cardView = Instantiate(cardViewPrefab, position, rotation);
        cardView.transform.SetParent(GameObject.Find("Shop_Foreground").transform);
        cardView.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        cardView.Setup(card);
        return cardView;
    }
    public ShopCardView CreateDeckerCardView(ShopCard card, Vector3 position, Quaternion rotation)
    {
        ShopCardView cardView = Instantiate(deckerViewPrefab, position + new Vector3(0, 140, 0), rotation);
        cardView.transform.SetParent(GameObject.Find("Decker").transform);
        cardView.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        cardView._cardButton.gameObject.SetActive(false);
        cardView.Setup(card);
        return cardView;
    }
    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
