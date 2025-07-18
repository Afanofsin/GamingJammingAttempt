using DG.Tweening;
using Mono.Cecil;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    [SerializeField]
    private HandView handView;
    [SerializeField]
    private Transform discardPilePos;
    [SerializeField]
    private Transform drawPilePos;

    public static CardSystem Instance;  

    public readonly List<Card> drawPile = new();
    public readonly List<Card> discardPile = new();
    private readonly List<Card> hand = new();

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
    }
    // Publics
    public void Setup(List<CardDataSO> deckData, int cardsDrawn)
    {
        foreach (var cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
        PileView.Instance.Setup(drawPilePos, discardPilePos, deckData.Count);
    }

    // Performers
    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int drawAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);
        int notDrawnAmount = drawCardsGA.Amount - drawAmount;
        for (int i = 0; i < drawAmount; i++)
        {
            yield return DrawCard();
        }
        if(notDrawnAmount > 0)
        {
            yield return RefillDeck();
            for (int i = 0; i < notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA) 
    {
        foreach(var card in hand)
        {
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        CardView cardView = handView.RemoveCard(playCardGA.Card);
        yield return DiscardCard(cardView);

        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA); 

        if(playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectsGA performEffectsGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(performEffectsGA);
        }

        foreach(var effect in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effect.TargetMode.GetTargets();
            PerformEffectsGA performEffectsGA = new(effect.Effect, targets);
            ActionSystem.Instance.AddReaction(performEffectsGA);
        }
    }
    // Reaction
    

    // Helpers
    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        if (card == null) yield break;
        if (handView.GetMaxCardNumber() == handView.GetCurrentCardNumber()) yield break;

        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePos.position, drawPilePos.rotation);

        PileView.Instance.OnCardRemovedFromDraw();

        Debug.Log("Card removed from Draw");
        yield return handView.AddCard(cardView);
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.Card);

        Debug.Log($"CardSystem: Added card to logical discard pile. Count is now: {discardPile.Count}");

        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePos.position, 0.15f);
        yield return tween.WaitForCompletion();

        PileView.Instance.ShowCardAddedToDiscard();
        Destroy(cardView);
    }

    private IEnumerator RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        yield return PileView.Instance.RepopulateDrawDeck();
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
