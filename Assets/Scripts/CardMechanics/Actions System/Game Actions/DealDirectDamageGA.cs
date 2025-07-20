using System.Collections.Generic;
using UnityEngine;

public class DealDirectDamageGA : GameAction, IHaveCaster
{
    public int Amount { get; set; }
    public List<CombatantView> Targets { get; set; }

    public CombatantView Caster { get; private set; }
    public DirectType DamageType { get; private set; }

    public DealDirectDamageGA(int amount, List<CombatantView> targets, CombatantView caster, DirectType type)
    {
        Amount = amount;
        Targets = new(targets);
        Caster = caster;
        DamageType = type;
    }
}

public enum DirectType
{
    IGNORESHIELD,
    MORALE,
    HEALTH
}
