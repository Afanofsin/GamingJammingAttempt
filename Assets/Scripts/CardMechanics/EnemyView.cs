using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField]
    private TMP_Text _attackText;

    public int AttackPower {  get; set; }

    public int Reward {  get; private set; }
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyPhase1State Phase1State { get; set; }
    public EnemyPhase2State Phase2State { get; set; }
    public EnemyPhase3State Phase3State { get; set; }
    public EnemyPhase1SOBase Phase1Instance { get; set; }
    public EnemyPhase2SOBase Phase2Instance { get; set; }
    public EnemyPhase3SOBase Phase3Instance { get; set; }


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

    public void UpdateAttackText()
    {
        _attackText.text = $"ATK:{AttackPower}";
    }
}
