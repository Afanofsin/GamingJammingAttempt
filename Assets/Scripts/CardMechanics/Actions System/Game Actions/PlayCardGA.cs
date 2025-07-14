using UnityEngine;

public class PlayCardGA : GameAction
{
    public Card Card { get; set; }

    public EnemyView ManualTarget { get; private set; }
    public PlayCardGA(Card card)
    {
        Card = card;
        ManualTarget = null;
    }

    public PlayCardGA(Card card, EnemyView manualTarget)
    {
        Card = card;
        ManualTarget = manualTarget;
    }
}
