using UnityEngine;

public class PerformEffectsGA : GameAction
{
    public Effect Effect {  get; set; }
    public PerformEffectsGA(Effect effect)
    {
        Effect = effect;
    }
}
