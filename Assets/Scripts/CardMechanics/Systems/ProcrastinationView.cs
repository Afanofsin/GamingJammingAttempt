using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcrastinationView : MonoBehaviour
{
    [SerializeField]
    private GameObject distractionPrefab;

    private List<GameObject> dist = new();
    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<AddStatusEffectGA>(CheckEffects, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<AddStatusEffectGA>(CheckEffects, ReactionTiming.POST);
    }

    private void CheckEffects(AddStatusEffectGA addStatusEffectGA)
    {
        if (addStatusEffectGA == null || addStatusEffectGA.Type == StatusEffectType.PROCRASTINATION) return;
        else StartCoroutine(ActivateProcrastination(addStatusEffectGA.Targets));
    }

    private IEnumerator ActivateProcrastination(List<CombatantView> targets)
    {
        foreach (var target in targets)
        {
            if (target.GetStatusEffectStacks(StatusEffectType.PROCRASTINATION) <= 0) yield break ;
        }

        foreach(var target in targets)
        {
            GameObject distObj = Instantiate(distractionPrefab, target.transform.position, Quaternion.identity);
            DistractionView distraction = distObj.GetComponent<DistractionView>();
            distraction.transform.parent = this.gameObject.transform;
            yield return CreateDistractions(distraction);
        }
    }

    private IEnumerator CreateDistractions(DistractionView distraction)
    {
        dist = distraction.distractions;
        foreach(var view in dist)
        {
            view.SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void OnReduceProc()
    {
        GameObject view = dist.LastOrDefault();
        view.SetActive(false);
    }
}
