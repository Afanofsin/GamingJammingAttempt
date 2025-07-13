using UnityEngine;
using System.Collections.Generic;

public abstract class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;
    private Transform playerTransform;
    [HideInInspector] public static InteractObject focusObject { get; private set; }
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.E) && IsWithinDistance() && focusObject == this)
        {
            Interact();
        }
    }
    public bool IsWithinDistance()
    {
        if (Vector2.Distance(transform.position, playerTransform.transform.position + new Vector3(-1f, -3f, 0)) <= 3)
        {
            return true;
        }

        return false;
    }
    public abstract void Interact();
    public abstract void DoAction();
    

}
