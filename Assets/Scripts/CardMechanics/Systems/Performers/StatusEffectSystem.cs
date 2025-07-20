using System.Collections;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{
    public static StatusEffectSystem Instance;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
    }

    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGA)
    {
        foreach (var target in addStatusEffectGA.Targets)
        {
            target.AddStatusEffect(addStatusEffectGA.Type, addStatusEffectGA.StackCount);
            yield return null; // ADD ANIMATIONS
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

public enum StatusEffectType
{
    SHIELD,
    BURN,
    PROCRASTINATION
}