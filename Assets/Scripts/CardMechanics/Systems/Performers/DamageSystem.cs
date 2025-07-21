using System.Collections;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public static DamageSystem Instance;

    [SerializeField]
    private GameObject damageVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.AttachPerformer<DealDirectDamageGA>(DealDirectDamagePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<DealDirectDamageGA>();
    }


    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach (var target in dealDamageGA.Targets)
        {
            target.Damage(dealDamageGA.Amount);

            Vector3 VFXpos = target.transform.position;
            VFXpos.y += 0.25f;
            VFXpos.x += 2f;
            if (damageVFX != null) Instantiate(damageVFX, VFXpos, Quaternion.identity);

            yield return new WaitForSeconds(0.15f);

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
        }
    }

    private IEnumerator DealDirectDamagePerformer(DealDirectDamageGA dealDirectDamageGA)
    {
        foreach (var target in dealDirectDamageGA.Targets)
        {
            target.DirectDamage(dealDirectDamageGA.Amount, dealDirectDamageGA.DamageType);

            Vector3 VFXpos = target.transform.position;
            VFXpos.y += 0.25f;
            VFXpos.x += 2f;
            if (damageVFX != null) Instantiate(damageVFX, VFXpos, Quaternion.identity);

            yield return new WaitForSeconds(0.15f);

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
