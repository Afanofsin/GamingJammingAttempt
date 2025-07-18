using System.Collections;
using UnityEngine;

public class BurnSystem : MonoBehaviour
{
    public static BurnSystem Instance;
    [SerializeField]
    private GameObject burnVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyBurnGA>();
    }

    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        if (burnVFX != null)
        {
            Instantiate(burnVFX, target.transform.position, Quaternion.identity);
        }
        target.Damage(applyBurnGA.BurnDamage);
        

        if(target.GetStatusEffectStacks(StatusEffectType.BURN) > 1)
        {
            target.RemoveStatusEffect(StatusEffectType.BURN, target.GetStatusEffectStacks(StatusEffectType.BURN) / 2);
        }
        else if (target.GetStatusEffectStacks(StatusEffectType.BURN) == 1)
        {
            target.RemoveStatusEffect(StatusEffectType.BURN, 1);
        }

        if (target.CurrentHealth <= 0)
        {
            if (target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }
            else
            {
                // Player Death Logic
            }
        }

        yield return null;
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
