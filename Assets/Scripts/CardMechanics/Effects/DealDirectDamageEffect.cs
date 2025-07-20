using System.Collections.Generic;
using UnityEngine;

public class DealDirectDamageEffect : Effect
{
    [SerializeField]
    private int damageAmount;
    [SerializeField]
    private DirectType type;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        DealDirectDamageGA dealDirectDamageGA = new(damageAmount, targets, caster, type);
        return dealDirectDamageGA;
    }
}
