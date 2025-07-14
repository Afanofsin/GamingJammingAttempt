using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField]
    private HeroDataSO _heroData;
    [SerializeField]
    private List<EnemyDataSO> _enemyDatas;

    public static MatchSetupSystem Instance;
    private void Start()
    {
        HeroSystem.Instance.Setup(_heroData);
        EnemySystem.Instance.Setup(_enemyDatas);
        CardSystem.Instance.Setup(_heroData.Deck);
        DrawCardsGA drawCardsGA = new(5);
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
