using UnityEngine;
using TMPro;
using System.Collections;

public class ManaSystem : MonoBehaviour
{
    public static ManaSystem Instance;

    [SerializeField]
    private ManaUI _manaUI;

    private const int MAX_MANA = 3;
    private int currentMana = MAX_MANA;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.AttachPerformer<AddManaGA>(AddManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
        ActionSystem.DetachPerformer<AddManaGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    // Performers

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.Amount;
        _manaUI.UpdateManaText(currentMana, MAX_MANA);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        currentMana = MAX_MANA;
        _manaUI.UpdateManaText(currentMana, MAX_MANA);
        yield return null;
    }

    private IEnumerator AddManaPerformer(AddManaGA addManaGA)
    {
        currentMana += addManaGA.Amount;
        _manaUI.UpdateManaText(currentMana, MAX_MANA);
        yield return null;
    }

    // Reactions

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.AddReaction(refillManaGA);
    }

    // Helper

    public bool HasEnoughMana(int manaCost)
    {
        return currentMana >= manaCost;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
