using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;
    private Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!IsWithinDistance() && _interactSprite.gameObject.activeSelf)
        {
            _interactSprite.gameObject.SetActive(false);

        }
        if (IsWithinDistance() && !_interactSprite.gameObject.activeSelf)
        {
            _interactSprite.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E) && IsWithinDistance())
        {
            Interact();
        }
    }
    public bool IsWithinDistance()
    {
        if (Vector2.Distance(transform.position, playerTransform.transform.position) <= 2)
        {

            return true;
        }

        return false;
    }
    public abstract void Interact();

}
