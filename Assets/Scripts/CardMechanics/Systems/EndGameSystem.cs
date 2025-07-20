using System.Collections;
using UnityEngine;

public class EndGameSystem : MonoBehaviour
{
    public static EndGameSystem Instance { get; private set; }
    [SerializeField]
    private EnemyBoardView enemyBoardView;
    private int reward;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<KillEnemyGA>(EnemyDeathReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<KillEnemyGA>(EnemyDeathReaction, ReactionTiming.POST);
    }

    private void EnemyDeathReaction(KillEnemyGA killEnemyGA)
    {
        reward += killEnemyGA.Reward;
        if (enemyBoardView.EnemyViews.Count == 0)
        {
            Debug.Log("You received " + reward + " money");
            StartCoroutine(StartSceneLeave());
        }
    }

    private IEnumerator StartSceneLeave()
    {
        yield return new WaitForSeconds(2.5f);
        GameManagerSystem.Instance.GoToHouseScene();
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
