using UnityEngine;
using System.Collections.Generic;

public abstract class InteractObject : MonoBehaviour, IInteractable, IDoAction
{
    [SerializeField] private SpriteRenderer _interactSprite;
    private Transform playerTransform;
    [HideInInspector] public static InteractObject focusObject { get; private set; }
    private const int INTERACT_RANGE = 3;

    /*#region StateMachine variables
    public StateMachine stateMachine { get; set; }     UNUSED
    public NPCIdleState idleState{ get; set; }
    #endregion*/
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        /*stateMachine = new StateMachine();
       idleState = new NPCIdleState(this, stateMachine);     UNUSED    */
    }
    /*private void Start()
    {
        stateMachine.Initialize(idleState);     UNUSED
    }*/

    void Update()
    {
        CheckDialogueEnd();
        if (!IsWithinDistance() && _interactSprite.gameObject.activeSelf && focusObject == this)
        {
            focusObject = null;
            _interactSprite.gameObject.SetActive(false);

        }
        if (IsWithinDistance() && !_interactSprite.gameObject.activeSelf && focusObject == null)
        {
            focusObject = this;
            _interactSprite.gameObject.SetActive(true);
        }
        if (focusObject != null && !focusObject.gameObject.activeSelf)
        {
            focusObject = null;
        }
        if (focusObject == null && _interactSprite.gameObject.activeSelf)
        {
            _interactSprite.gameObject.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.E) && IsWithinDistance() && focusObject == this)
        {
            Interact();
        }
        
    }
    public bool IsWithinDistance()
    {
        if (Vector2.Distance(transform.position, playerTransform.transform.position) <= INTERACT_RANGE)
        {
            return true;
        }

        return false;
    }
    public abstract void Interact();
    public abstract void DoAction();

    public abstract void CheckDialogueEnd();
    
    /* private void AnimationTriggerEvent(AnimationTrigger triggerType)
    {
        stateMachine.CurrentNPCState.AnimationTriggerEvent(triggerType);     UNUSED
    }
    public enum AnimationTrigger
    {
        IdleSound,
        ActionSound
    } */

}
