using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using static GameManagerSystem;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField]
    private HeroDataSO _heroData;
    [SerializeField]
    private List<EnemyDataSO> _enemyDatas;
    [SerializeField]
    private int cardsToDrawAtStart = 5;

    public static MatchSetupSystem Instance;

    private void OnEnable()
    {
        GameManagerSystem.OnSceneReady += SetupCheck;
    }
    /*private void Start()
    {
        StartCoroutine(MatchSetup(GameManagerSystem.Instance.GetEnemiesForBattle()));
    }*/
    private void OnDisable()
    {
        GameManagerSystem.OnSceneReady -= SetupCheck;
    }

    private void SetupCheck(GameState gameState)
    {
        if (gameState == GameState.InBattle) StartCoroutine(MatchSetup(GameManagerSystem.Instance.GetEnemiesForBattle()));
        else return;
    }

    public IEnumerator MatchSetup(List<EnemyDataSO> enemies)
    {
        HeroSystem.Instance.Setup(_heroData);
        EnemySystem.Instance.Setup(enemies);
        CardSystem.Instance.Setup(_heroData.Deck, cardsToDrawAtStart);

        yield return new WaitUntil(() => !ActionSystem.Instance.isPerforming);

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
