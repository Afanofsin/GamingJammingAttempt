using DG.Tweening;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HeroSystem : MonoBehaviour
{
    [field: SerializeField]
    public HeroView HeroView {  get; private set; }

    public static HeroSystem Instance;

    private Coroutine returnCoroutine;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddMoraleGA>(AddMoralePerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<PlayCardGA>(PlayCardAnimation, ReactionTiming.PRE);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddMoraleGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        ActionSystem.UnsubscribeReaction<PlayCardGA>(PlayCardAnimation, ReactionTiming.PRE);
    }

    public void Setup(HeroDataSO heroData)
    {
        HeroView.Setup(heroData);

    }
    // Performers
    private IEnumerator AddMoralePerformer(AddMoraleGA addMoraleGA)
    {
        HeroView.AddMorale(addMoraleGA.Amount);
        yield return null;
    }

    // Reactions
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        int burnStacks = HeroView.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0) 
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }

    private void PlayCardAnimation(PlayCardGA playCardGA)
    {
        if (HeroView.Animator)
        {
            HeroView.Animator.SetTrigger("Attack");
            StartCoroutine(MoveHero());
        }
        
    }

    private IEnumerator MoveHero()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        HeroView.transform.DOKill();
        Tween tween = HeroView.transform
            .DOMoveX(HeroView.transform.position.x + 2.5f, 0.35f)
            .SetEase(Ease.OutQuint);
        yield return tween.WaitForCompletion();
        HeroView.transform.DOMove(HeroView.HeroPos, 0.1f);
        returnCoroutine = StartCoroutine(ReturnHeroAfterDelay());
    }

    private IEnumerator ReturnHeroAfterDelay()
    {
        // Wait for a short period of inactivity
        yield return new WaitForSeconds(0.8f);

        // Now, move the hero back to their starting position
        HeroView.transform.DOMove(HeroView.HeroPos, 0.2f).SetEase(Ease.InOutSine);
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
