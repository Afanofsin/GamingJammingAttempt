using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField]
    private SplineContainer _splineContainer;
    [SerializeField]
    private int maxHandSize = 8;

    private readonly List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardsPositions(0.15f);
    }

    private IEnumerator UpdateCardsPositions(float duration)
    {
        if (cards.Count == 0) yield break;
        float cardSpacing = 1f / (float)maxHandSize;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;

        Spline spline = _splineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            float pos = firstCardPosition + (i * cardSpacing);
            Vector3 splinePosition = _splineContainer.EvaluatePosition(pos);
            Vector3 forward = spline.EvaluateTangent(pos);
            Vector3 up = spline.EvaluateUpVector(pos);

            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition, 0.25f);
            cards[i].transform.DORotateQuaternion(rotation, 0.25f);


        }
        yield return new WaitForSeconds(duration);
    }

    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if(cardView == null) return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardsPositions(0.15f));
        return cardView;
    }

    private CardView GetCardView(Card card)
    {
        return cards.Where(cardView => cardView.Card == card).FirstOrDefault();
    }
}
