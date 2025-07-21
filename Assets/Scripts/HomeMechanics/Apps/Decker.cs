using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Decker : MonoBehaviour
{
    [SerializeField] private Movement _player;
    [SerializeField] private List<Card> playerNewInventoryDeck;
    [SerializeField] private List<Card> playerNewBattleDeck;
    [SerializeField] private Button inventoryCard;
    [SerializeField] private Button battleCard;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    private const int MINIMUM_BATTLE = 12;
    private const int MAXIMUM_BATTLE = 20;
    private string cardToTransfer;
    private GameObject[] objectsToDestroy1;
    private GameObject[] objectsToDestroy2;

    void OnEnable()
    {
        Debug.Log(_player.BattleDeck.Count);
        Debug.Log(_player.InventoryDeck.Count);
        cardToTransfer = null;
        Destroy(GameObject.FindGameObjectWithTag("ShopCard"));
        rightButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        playerNewBattleDeck = new();
        playerNewInventoryDeck = new();
        playerNewBattleDeck.AddRange(_player.BattleDeck);
        playerNewInventoryDeck.AddRange(_player.InventoryDeck);
        if (playerNewInventoryDeck != null)
        {
            FillInventory();
            FillBattle();
        }
        objectsToDestroy1 = GameObject.FindGameObjectsWithTag("InventoryButton");
        objectsToDestroy2 = GameObject.FindGameObjectsWithTag("BattleButton");
    }
    void Update()
    {
        //Debug.Log(cardToTransfer);
    }
    public void ShowCardLeft(string cardTitle)
    {
        if (GameObject.FindGameObjectWithTag("ShopCard") != null)
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("ShopCard"));
        }
        ShopCardViewCreator.Instance.CreateDeckerCardView(new ShopCard(CardManagementSystem.Instance.GetCardByName(cardTitle)), transform.position, quaternion.identity);
        cardToTransfer = null;
        if (playerNewBattleDeck.Count < MAXIMUM_BATTLE)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
            cardToTransfer = GameObject.FindGameObjectWithTag("ShopCard").GetComponent<ShopCardView>()._cardNameText.text;
        }
        else
        {
            cardToTransfer = null;
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
        }

    }
    public void ShowCardRight(string cardTitle)
    {
        cardToTransfer = null;
        if (GameObject.FindGameObjectWithTag("ShopCard") != null)
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("ShopCard"));
        }
        ShopCardViewCreator.Instance.CreateDeckerCardView(new ShopCard(CardManagementSystem.Instance.GetCardByName(cardTitle)), transform.position, quaternion.identity);
        if (playerNewBattleDeck.Count > MINIMUM_BATTLE)
        {
            cardToTransfer = GameObject.FindGameObjectWithTag("ShopCard").GetComponent<ShopCardView>()._cardNameText.text;
            rightButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(true);
        }
        else
        {
            cardToTransfer = null;
            rightButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(false);
        }

    }
    public void TransferCard()
    {
        if (string.IsNullOrEmpty(cardToTransfer))
            return;

        Transform inventoryParent = GameObject.Find("InventoryDeckContent").transform;
        Transform battleParent = GameObject.Find("BattleDeckContent").transform;

        if (rightButton.gameObject.activeSelf)
        {
            Card card = playerNewInventoryDeck.Find(c => c.Title == cardToTransfer);
            if (card != null)
            {
                playerNewInventoryDeck.Remove(card);
                playerNewBattleDeck.Add(card);

                DestroyButtonWithName(inventoryParent, cardToTransfer);
                CreateBattleDeckButton(card);
            }

            rightButton.gameObject.SetActive(false);
        }
        else if (leftButton.gameObject.activeSelf)
        {
            Card card = playerNewBattleDeck.Find(c => c.Title == cardToTransfer);
            if (card != null)
            {
                playerNewBattleDeck.Remove(card);
                playerNewInventoryDeck.Add(card);

                DestroyButtonWithName(battleParent, cardToTransfer);
                CreateInventoryDeckButton(card);
            }

            leftButton.gameObject.SetActive(false);
        }
        objectsToDestroy1 = GameObject.FindGameObjectsWithTag("InventoryButton");
        objectsToDestroy2 = GameObject.FindGameObjectsWithTag("BattleButton");
        rightButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("ShopCard"));
        cardToTransfer = null;
    }
    public void FillInventory()
    {
        foreach (Card card in playerNewInventoryDeck)
        {
            var newButton = Instantiate(inventoryCard, inventoryCard.transform.position, quaternion.identity);
            newButton.transform.SetParent(GameObject.Find("InventoryDeckContent").transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = card.Title;
            newButton.transform.localScale = new Vector3(1, 1, 1);
            string cardTitle = card.Title;
            newButton.onClick.AddListener(() => ShowCardLeft(cardTitle));
        }
    }
    public void FillBattle()
    {
        foreach (Card card in playerNewBattleDeck)
        {
            var newButton = Instantiate(battleCard, battleCard.transform.position, quaternion.identity);
            newButton.transform.SetParent(GameObject.Find("BattleDeckContent").transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = card.Title;
            newButton.transform.localScale = new Vector3(1, 1, 1);
            string cardTitle = card.Title;
            newButton.onClick.AddListener(() => ShowCardRight(cardTitle));
        }
    }
    private void DestroyButtonWithName(Transform parent, string cardName)
    {
        foreach (Transform child in parent)
        {
            TextMeshProUGUI text = child.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null && text.text == cardName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    private void CreateInventoryDeckButton(Card card)
    {
        var newButton = Instantiate(inventoryCard, inventoryCard.transform.position, quaternion.identity);
        newButton.transform.SetParent(GameObject.Find("InventoryDeckContent").transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = card.Title;
        newButton.transform.localScale = Vector3.one;
        string cardTitle = card.Title;
        newButton.onClick.AddListener(() => ShowCardLeft(cardTitle));
    }

    private void CreateBattleDeckButton(Card card)
    {
        var newButton = Instantiate(battleCard, battleCard.transform.position, quaternion.identity);
        newButton.transform.SetParent(GameObject.Find("BattleDeckContent").transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = card.Title;
        newButton.transform.localScale = Vector3.one;
        string cardTitle = card.Title;
        newButton.onClick.AddListener(() => ShowCardRight(cardTitle));
    }
    public void SwtichDecker()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
    void OnDisable()
    {
        foreach (var go in objectsToDestroy1)
        {
            Destroy(go);
        }
        foreach (var go in objectsToDestroy2)
        {
            Destroy(go);
        }
        _player.defaultHero.RebuildBattleDeck(playerNewBattleDeck);
        _player.defaultHero.RebuildInventoryDeck(playerNewInventoryDeck);
        _player.BattleDeck = new();
        _player.InventoryDeck = new();
        _player.BattleDeck.AddRange(playerNewBattleDeck);
        _player.InventoryDeck.AddRange(playerNewInventoryDeck);
    }
}
