using UnityEngine;

public class InteractionsSystem : MonoBehaviour
{
    public static InteractionsSystem Instance;

    public bool PlayerIsDraggin { get; set; } = false;
    public bool PlayerCanInteract()
    {
        if (!ActionSystem.Instance.isPerforming) return true;
        else return false;
    }

    public bool PlayerCanHover()
    {
        if (PlayerIsDraggin) return false;
        else return true;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}




