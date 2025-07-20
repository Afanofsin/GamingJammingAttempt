using System.Collections.Generic;
using UnityEngine;

public class PerformEffectsGA : GameAction, IHaveCaster
{
    public Effect Effect {  get; set; }
    public List<CombatantView> Targets { get; set; }

    public CombatantView Caster {  get; set; }

    public PerformEffectsGA(Effect effect, List<CombatantView> targets, CombatantView caster)
    {
        Effect = effect;
        Targets = targets == null ? null : new(targets);
        Caster = caster;
    }
}
