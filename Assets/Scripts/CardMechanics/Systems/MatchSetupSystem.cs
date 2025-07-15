using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField]
    private HeroDataSO _heroData;
    [SerializeField]
    private List<EnemyDataSO> _enemyDatas;
    [SerializeField]
    private int cardsToDrawAtStart = 5;

    public static MatchSetupSystem Instance;
    private void Start()
    {
        HeroSystem.Instance.Setup(_heroData);
        EnemySystem.Instance.Setup(_enemyDatas);
        CardSystem.Instance.Setup(_heroData.Deck, cardsToDrawAtStart);
        DrawCardsGA drawCardsGA = new(cardsToDrawAtStart);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
