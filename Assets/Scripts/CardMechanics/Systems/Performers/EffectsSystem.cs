using System.Collections;
using UnityEngine;

public class EffectsSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectsGA>(PerformEffectPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectsGA>();
    }

    // Performer
    private IEnumerator PerformEffectPerformer(PerformEffectsGA performEffectsGA)
    {
        GameAction effectAction = performEffectsGA.Effect.GetGameAction(performEffectsGA.Targets, performEffectsGA.Caster);
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
}
