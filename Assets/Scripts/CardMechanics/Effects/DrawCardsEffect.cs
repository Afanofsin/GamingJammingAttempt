using System.Collections.Generic;
using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField]
    private int drawAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        DrawCardsGA drawCardsGA = new(drawAmount);
        return drawCardsGA;
    }
}
