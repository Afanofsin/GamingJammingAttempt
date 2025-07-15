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
    private float cardSpacing = 0.05f;

    private readonly List<PileCardView> drawPile = new();
    private readonly List<PileCardView> discardPile = new();

    private Transform drawPos;
    private Transform discardPos;

    public void Setup(Transform drawPilePos, Transform discardPilePos, int drawPileCount)
    {
        drawPos = drawPilePos;
        discardPos = discardPilePos;
        StartCoroutine(SetupDrawPile(drawPileCount));
    }

    private IEnumerator SetupDrawPile(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return AddCardToDraw();
        }

       yield return UpdateDrawPile(count);
    }

    public IEnumerator AddCardToDiscard()
    {
        yield return new WaitForSeconds(0.15f);
        PileCardView pileCardView = CreatePileCardView(discardPos, Quaternion.identity);
        discardPile.Add(pileCardView);
        yield return UpdateDiscardPile(discardPile.Count);
    }

    public PileCardView RemoveCardFromDiscard()
    {
        PileCardView pileCardView = discardPile.Last();
        if (pileCardView == null) return null;
        discardPile.Remove(pileCardView);
        StartCoroutine(UpdateDiscardPile(discardPile.Count));
        return pileCardView;
    }

    public PileCardView CreatePileCardView(Transform parent, Quaternion rotation)
    {
        PileCardView pileCardView = Instantiate(cardBackPrefab, Vector3.zero, rotation);
        pileCardView.transform.parent = parent;
        pileCardView.transform.localScale = Vector3.one;
        pileCardView.transform.DOScale(Vector3.one, 0.15f);
        return pileCardView;
    }
    public IEnumerator AddCardToDraw()
    {
        yield return new WaitForSeconds(0.15f);
        PileCardView pileCardView = CreatePileCardView(drawPos, Quaternion.identity);
        drawPile.Add(pileCardView);
        yield return UpdateDrawPile(drawPile.Count);
    }
    public PileCardView RemoveCardFromDraw()
    {
        PileCardView pileCardView = drawPile.Last();
        if (pileCardView == null) return null;
        drawPile.Remove(pileCardView);
        StartCoroutine(UpdateDiscardPile(drawPile.Count));
        return pileCardView;
    }

    public IEnumerator UpdateDrawPile(int count)
    {
        if (count == 0) yield break;

        int visibleCards = Mathf.Min(MAX_CARDS_VISIBLE, count);

        for(int i = 0; i < visibleCards; i++)
        {
            Vector3 endPos = drawPos.position;
            endPos.x += cardSpacing * i;
            drawPile[i].transform.position = endPos;
            drawPile[i].transform.rotation = Quaternion.identity;

            if (i < visibleCards - 1)
            {
                drawPile[i].TurnOffAnimation();
                drawPile[i].MakeSpriteOnBottom();
            }
        }

        if (visibleCards > 0)
        {
            drawPile[visibleCards - 1].TurnOnAnimation();
            drawPile[visibleCards - 1].MakeSpriteOnTop();
        }
            
    }

    public IEnumerator UpdateDiscardPile(int count)
    {
        if (count == 0) yield break;

        int visibleCards = Mathf.Min(MAX_CARDS_VISIBLE, count);

        for (int i = 0; i < visibleCards; i++)
        {
            Vector3 endPos = discardPos.position;
            endPos.x += cardSpacing * i;
            discardPile[i].transform.position = endPos;
            discardPile[i].transform.rotation = Quaternion.identity;

            if (i < visibleCards - 1)
            {
                discardPile[i].TurnOffAnimation();
                discardPile[i].MakeSpriteOnBottom();
            }
        }

        if (visibleCards > 0)
        {
            discardPile[visibleCards - 1].TurnOnAnimation();
            discardPile[visibleCards - 1].MakeSpriteOnTop();
        }
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
