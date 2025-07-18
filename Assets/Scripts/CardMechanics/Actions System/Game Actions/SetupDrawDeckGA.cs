using UnityEngine;

public class SetupDrawDeckGA : GameAction
{
    public int CardAmount {  get; private set; }

    public SetupDrawDeckGA(int cardAmount)
    {
        CardAmount = cardAmount;
    }
}
