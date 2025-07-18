using UnityEngine;

public class AddMoraleGA : GameAction
{
    public int Amount {  get; private set; }

    public AddMoraleGA(int amount)
    {
        Amount = amount;
    }
}
