using System.Collections.Generic;
using UnityEngine;

public class AddMoraleEffect : Effect
{
    [SerializeField]
    private int amount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        AddMoraleGA addMoraleGA = new(amount);
        return addMoraleGA;
    }
}
