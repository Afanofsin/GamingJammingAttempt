using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private Button buttonComponent;
    [SerializeField]
    private Sprite turnReady;
    [SerializeField]
    private Sprite turnInProgress;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnStart, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnEnd, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnStart, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnEnd, ReactionTiming.POST);
    }

    public void OnClick()
    {
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }

    private void EnemyTurnStart(EnemyTurnGA enemyTurnGA)
    {
        buttonComponent.interactable = false;
        buttonImage.sprite = turnInProgress;
    }

    private void EnemyTurnEnd(EnemyTurnGA enemyTurnGA)
    {
        buttonComponent.interactable = true;
        buttonImage.sprite = turnReady;
    }
}
