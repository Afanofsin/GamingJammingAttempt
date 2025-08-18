using UnityEngine;

public class MoneyDamageGA : GameAction
{
    public float Amount { get; set; }
    public HeroView Target { get; set; }
    public CombatantView Caster { get; private set; }

    public MoneyDamageGA(float amount, HeroView target,  CombatantView caster)
    {
        Amount = amount;
        Target = target;
        Caster = caster;
    }
}

