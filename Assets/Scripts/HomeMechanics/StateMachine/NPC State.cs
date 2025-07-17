using UnityEngine;

public class NPCState
{
    protected InteractObject interactObject;
    protected StateMachine stateMachine;
    public NPCState(InteractObject interactObject, StateMachine stateMachine)
    {
        this.interactObject = interactObject;
        this.stateMachine = stateMachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
}
