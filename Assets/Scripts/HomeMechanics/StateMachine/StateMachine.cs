using System;
using UnityEngine;

public class StateMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public  NPCState CurrentNPCState { get; set; }
    public void Initialize(NPCState startingState)
    {
        CurrentNPCState = startingState;
        CurrentNPCState.EnterState();
    }
    public void ChangeState(NPCState newState)
    {
        CurrentNPCState.ExitState();
        CurrentNPCState = newState;
        CurrentNPCState.EnterState();
    }

    internal static void Initialize()
    {
        throw new NotImplementedException();
    }
}
