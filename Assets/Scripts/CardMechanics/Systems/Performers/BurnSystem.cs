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
            Vector3 VFXpos = target.transform.position;
            Quaternion VFXrot = Quaternion.Euler(0f, 0f, -180f);
            VFXpos.y += 10f;
            VFXpos.x -= 1f;
            Instantiate(burnVFX, VFXpos, VFXrot);
        }
        target.DirectDamage(applyBurnGA.BurnDamage, DirectType.IGNORESHIELD);
        

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
                KillEnemyGA killEnemyGA = new(enemyView, enemyView.Reward);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }
            else
            {
                PlayerDeathGA playerDeathGA = new();
                ActionSystem.Instance.AddReaction(playerDeathGA);
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
