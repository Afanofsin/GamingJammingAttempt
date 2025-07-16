using UnityEngine;

public class NPCIdleState : NPCState
{
    public NPCIdleState(InteractObject interactObject, StateMachine stateMachine) : base(interactObject, stateMachine)
    {

    }
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void AnimationTriggerEvent(InteractObject.AnimationTrigger animationTrigger)
    {
        base.AnimationTriggerEvent(animationTrigger);
    }
}
