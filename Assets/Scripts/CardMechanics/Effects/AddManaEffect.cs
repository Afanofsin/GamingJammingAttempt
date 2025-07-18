using System.Collections.Generic;
using UnityEngine;

public class AddManaEffect : Effect
{
    [SerializeField]
    private int amount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        AddManaGA addManaGA = new(amount);
        return addManaGA;
    }
}
