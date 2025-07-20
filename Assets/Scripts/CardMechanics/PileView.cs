using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.GPUSort;

public class PileView : MonoBehaviour
{
    public static PileView Instance { get; private set; }

    [SerializeField]
    private PileCardView cardBackPrefab;
    [SerializeField]
    private int MAX_CARDS_VISIBLE = 8;
    [SerializeField]
    private float cardSpacing = 0.0625f;

    private readonly List<PileCardView> drawPileVisuals = new();
    private readonly List<PileCardView> discardPileVisuals = new();

    private Transform drawPos;
    private Transform discardPos;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SetupDrawDeckGA>(SetupDrawPilePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SetupDrawDeckGA>();
    }

    public void Setup(Transform drawPilePos, Transform discardPilePos, int drawPileCount)
    {
        drawPos = drawPilePos;
        discardPos = discardPilePos;

        InitializeVisualPools();

        SetupDrawDeckGA setupDrawDeckGA = new(drawPileCount);
        ActionSystem.Instance.Perform(setupDrawDeckGA);
    }

    private void InitializeVisualPools()
    {
        // Clear existing visuals if any
        ClearVisualPools();

        // Create fixed visual cards for draw pile
        for (int i = 0; i < MAX_CARDS_VISIBLE; i++)
        {
            PileCardView visualCard = CreatePileCardView(drawPos);
            drawPileVisuals.Add(visualCard);
            visualCard.gameObject.SetActive(false);
        }

        // Create fixed visual cards for discard pile
        for (int i = 0; i < MAX_CARDS_VISIBLE; i++)
        {
            PileCardView visualCard = CreatePileCardView(discardPos);
            discardPileVisuals.Add(visualCard);
            visualCard.gameObject.SetActive(false);
        }
    }

    private void ClearVisualPools()
    {
        foreach (var card in drawPileVisuals)
        {
            if (card != null) Destroy(card.gameObject);
        }
        foreach (var card in discardPileVisuals)
        {
            if (card != null) Destroy(card.gameObject);
        }
        drawPileVisuals.Clear();
        discardPileVisuals.Clear();
    }

    private IEnumerator SetupDrawPilePerformer(SetupDrawDeckGA setupDrawDeckGA)
    {
        for (int i = 0; i < setupDrawDeckGA.CardAmount; i++)
        {
            //yield return AddCardToDraw();
            yield return ShowCardAddedToDraw();
        }

       yield return new WaitForSeconds(0.15f);
    }

    public void ShowCardAddedToDiscard()
    {
        UpdateDiscardPileVisuals();
    }

    public void OnCardRemovedFromDiscard()
    {
        UpdateDiscardPileVisuals();
    }

    public IEnumerator ShowCardAddedToDraw()
    {
        yield return UpdateDrawPileVisuals(0.05f);
    }

    public void OnCardRemovedFromDraw()
    {
        StartCoroutine(UpdateDrawPileVisuals(0.05f));
    }

    private IEnumerator UpdateDrawPileVisuals(float duration)
    {
        int cardCount = CardSystem.Instance.drawPile.Count;
        int visibleCards = Mathf.Min(MAX_CARDS_VISIBLE, cardCount);

        // Update visual cards
        for (int i = 0; i < MAX_CARDS_VISIBLE; i++)
        {
            PileCardView visualCard = drawPileVisuals[i];

            if (i < visibleCards)
            {
                visualCard.gameObject.SetActive(true);

                Vector3 endPos = drawPos.position;
                endPos.x += cardSpacing * i;
                visualCard.transform.position = endPos;
                visualCard.transform.rotation = Quaternion.identity;

                if (i < visibleCards - 1)
                {
                    visualCard.TurnOffAnimation();
                    visualCard.MakeSpriteOnBottom();
                }
                else
                {
                    // Top card
                    visualCard.TurnOnAnimation();
                    visualCard.MakeSpriteOnTop();
                }
            }
            else
            {
                // Hide this visual card
                visualCard.gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(duration);
    }

    private void UpdateDiscardPileVisuals()
    {
        // Get actual count from CardSystem
        int cardCount = CardSystem.Instance.discardPile.Count;
        int visibleCards = Mathf.Min(MAX_CARDS_VISIBLE, cardCount);

        // Update visual cards
        for (int i = 0; i < MAX_CARDS_VISIBLE; i++)
        {
            PileCardView visualCard = discardPileVisuals[i];

            if (i < visibleCards)
            {
                visualCard.gameObject.SetActive(true);

                Vector3 endPos = discardPos.position;
                endPos.x -= cardSpacing * i;
                visualCard.transform.position = endPos;
                visualCard.transform.rotation = Quaternion.identity;

                if (i < visibleCards - 1)
                {
                    visualCard.TurnOffAnimation();
                    visualCard.MakeSpriteOnBottom();
                }
                else
                {
                    // Top card
                    visualCard.TurnOnAnimation();
                    visualCard.MakeSpriteOnTop();
                }
            }
            else
            {
                // Hide this visual card
                visualCard.gameObject.SetActive(false);
            }
        }
    }

    public PileCardView CreatePileCardView(Transform parent)
    {
        PileCardView pileCardView = Instantiate(cardBackPrefab, Vector3.zero, Quaternion.identity);
        pileCardView.transform.parent = parent;
        pileCardView.transform.localScale = Vector3.one;
        return pileCardView;
    }

    public IEnumerator RepopulateDrawDeck()
    {
        List<PileCardView> cardsToAnimate = discardPileVisuals.Where(card => card.gameObject.activeInHierarchy).ToList();

        if (cardsToAnimate.Count == 0)
        {
            // No cards to animate, so just update the visuals and exit.
            UpdateDiscardPileVisuals();
            StartCoroutine(UpdateDrawPileVisuals(0f)); // Use StartCoroutine as it returns IEnumerator
            yield break; // Exit the coroutine
        }

        Vector3 offScreenRight = discardPos.position + new Vector3(5f, 0, 0); 
        Vector3 offScreenLeft = drawPos.position - new Vector3(5f, 0, 0);

        Sequence refillSequence = DOTween.Sequence();
        refillSequence.AppendInterval(0.2f);

        for (int i = 0; i < cardsToAnimate.Count; i++)
        {
            PileCardView card = cardsToAnimate[i];
            card.MakeSpriteOnTop(); // Ensure it's visible

            // Create a mini-sequence for each individual card's journey.
            Sequence cardJourney = DOTween.Sequence();

            cardJourney.Append(
                // Part 1: Fly from discard pile to off-screen right
                card.transform.DOMove(offScreenRight, 0.35f).SetEase(Ease.InQuad)
            ).AppendCallback(() => {
                // Part 2: Instantly teleport the card to off-screen left
                // This happens between animations, so the player won't see the pop.
                card.transform.position = offScreenLeft;
            }).Append(
                // Part 3: Fly from off-screen left to the draw pile
                card.transform.DOMove(drawPos.position, 0.35f).SetEase(Ease.OutQuad)
            );

            // Add this card's personal journey to the main sequence.
            // We use Join so all cards can perform their journey at the same time,
            // but the delay still creates the cascade effect.
            refillSequence.Join(cardJourney.SetDelay(i * 0.06f));
        }

        // 4. Clean up AFTER the animation is done (same as before).
        refillSequence.AppendCallback(() => {
            UpdateDiscardPileVisuals();
            StartCoroutine(UpdateDrawPileVisuals(0f));
        });

        // 5. Wait for the entire sequence to complete (same as before).
        yield return refillSequence.WaitForCompletion();

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
