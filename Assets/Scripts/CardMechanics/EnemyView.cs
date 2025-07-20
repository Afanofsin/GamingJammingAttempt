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
    private float distractionRadius = 1.2f;
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
        // We only care if the effect was applied to THIS enemy.
        if (!ga.Targets.Contains(this)) return;

        // We only care if the effect being added IS Procrastination.
        if (ga.Type == StatusEffectType.PROCRASTINATION)
        {
            // The effect was just applied, so create the visuals.
            // We get the stacks AFTER the action has completed.
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
            distObj.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
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

        // Optional: Re-arrange the positions of the remaining distractions
        // so they look nice (e.g., in an arc over the enemy's head).
        ArrangeDistractions();
    }

    private void ArrangeDistractions()
    {
        for (int i = 0; i < activeDistractions.Count; i++)
        {
            // 1. Get a random angle in radians.
            // A full circle is 2 * PI radians.
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            // 2. Get a random distance within the radius.
            // Using Random.Range(0, radius) would cluster points near the center.
            // Multiplying by sqrt(random) gives a much more uniform distribution across the circle's area.
            float randomRadius = distractionRadius * Mathf.Sqrt(Random.Range(0f, 1f));

            // 3. Convert the circular coordinates (angle + radius) to cartesian coordinates (x, y).
            float x = Mathf.Cos(randomAngle) * randomRadius;
            float y = Mathf.Sin(randomAngle) * randomRadius;

            // 4. Create the final position vector and apply it.
            // We use localPosition so the placement is relative to the distractionParent.
            Vector3 newPosition = new Vector3(x, y, 0);
            activeDistractions[i].transform.localPosition = newPosition;
        }
    }


    public void UpdateAttackText()
    {
        _attackText.text = $"ATK:{AttackPower}";
    }
}
