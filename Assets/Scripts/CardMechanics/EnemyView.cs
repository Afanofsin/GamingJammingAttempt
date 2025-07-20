using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField]
    private TMP_Text _attackText;
    [SerializeField]
    private GameObject distractionPrefab; // The visual prefab for one distraction
    [SerializeField]
    private Transform distractionParent; // An empty GameObject child of EnemyView to hold the distractions
    [SerializeField]
    private List<Transform> spawnPoints = new();
    public int AttackPower {  get; set; }

    public int Reward {  get; private set; }
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyPhase1State Phase1State { get; set; }
    public EnemyPhase2State Phase2State { get; set; }
    public EnemyPhase3State Phase3State { get; set; }
    public EnemyPhase1SOBase Phase1Instance { get; set; }
    public EnemyPhase2SOBase Phase2Instance { get; set; }
    public EnemyPhase3SOBase Phase3Instance { get; set; }

    private readonly List<GameObject> activeDistractions = new();

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<PlayCardGA>(ReduceProcrastination, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<AddStatusEffectGA>(OnStatusEffectAdded, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<PlayCardGA>(ReduceProcrastination, ReactionTiming.POST);
        ActionSystem.UnsubscribeReaction<AddStatusEffectGA>(OnStatusEffectAdded, ReactionTiming.POST);
    }

    public void Setup(EnemyDataSO enemyDataSO)
    {
        AttackPower = enemyDataSO.AttackPower;
        UpdateAttackText();
        Phase1Instance = Instantiate(enemyDataSO.Phase1Base);
        Phase2Instance = Instantiate(enemyDataSO.Phase2Base);
        Phase3Instance = Instantiate(enemyDataSO.Phase3Base);

        StateMachine = new EnemyStateMachine();
        Phase1State = new EnemyPhase1State(this, StateMachine);
        Phase2State = new EnemyPhase2State(this, StateMachine);
        Phase3State = new EnemyPhase3State(this, StateMachine);

        Phase1Instance.Initialize(this);
        Phase2Instance.Initialize(this);
        Phase3Instance.Initialize(this);

        StateMachine.Initialize(Phase1State);
        Reward = enemyDataSO.Reward;
        SetupBase(enemyDataSO.Health, enemyDataSO.Morale, enemyDataSO.Image, enemyDataSO.Controller);
    }

    public void ReduceProcrastination(PlayCardGA playCardGA)
    {
        if (GetStatusEffectStacks(StatusEffectType.PROCRASTINATION) <= 0) return;
        RemoveStatusEffect(StatusEffectType.PROCRASTINATION, 1);
        UpdateDistractionVisuals();
    }

    private void OnStatusEffectAdded(AddStatusEffectGA ga)
    {
        if (!ga.Targets.Contains(this)) return;

        if (ga.Type == StatusEffectType.PROCRASTINATION)
        {
            UpdateDistractionVisuals();
        }
    }

    public void UpdateDistractionVisuals()
    {
        int currentStacks = GetStatusEffectStacks(StatusEffectType.PROCRASTINATION);

        // --- Create new distractions if needed ---
        while (activeDistractions.Count < currentStacks)
        {
            // Use the distractionParent transform so they are neatly organized in the hierarchy.
            GameObject distObj = Instantiate(distractionPrefab, distractionParent.position, Quaternion.identity, distractionParent);
            activeDistractions.Add(distObj);
            distObj.transform.localScale = Vector3.zero;
            distObj.transform.DOScale(new Vector3(2f,2f,2f), 0.3f).SetEase(Ease.OutBack);
        }

        // --- Remove extra distractions if needed ---
        while (activeDistractions.Count > currentStacks)
        {
            GameObject distToRemove = activeDistractions[activeDistractions.Count - 1];
            activeDistractions.RemoveAt(activeDistractions.Count - 1);
            // Destroy the GameObject.
            Destroy(distToRemove);
            // You could add a "pop-out" animation here too.
        }
        ArrangeDistractions();
    }

    private void ArrangeDistractions()
    {
        int pointsToUse = Mathf.Min(activeDistractions.Count, spawnPoints.Count);
        for (int i = 0; i < pointsToUse; i++)
        {
            activeDistractions[i].transform.localPosition = spawnPoints[i].localPosition;
        }
    }


    public void UpdateAttackText()
    {
        _attackText.text = $"ATK:{AttackPower}";
    }
}
