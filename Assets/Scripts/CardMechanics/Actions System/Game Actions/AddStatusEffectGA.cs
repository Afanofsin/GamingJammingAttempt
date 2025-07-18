using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffectGA : GameAction
{
    public StatusEffectType Type;
    public int StackCount {  get; private set; }
    public List<CombatantView> Targets { get; private set; }

    public AddStatusEffectGA(StatusEffectType type, int stackCount, List<CombatantView> combatants)
    {
        Type = type;
        StackCount = stackCount;
        Targets = combatants;
    }
}
