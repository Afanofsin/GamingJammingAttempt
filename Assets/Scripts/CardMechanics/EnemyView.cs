using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField]
    private TMP_Text _attackText;

    public int AttackPower {  get; set; }

    public void Setup(EnemyDataSO enemyDataSO)
    {
        AttackPower = enemyDataSO.AttackPower;
        UpdateAttackText();
        SetupBase(enemyDataSO.Health, enemyDataSO.Image);
    }

    private void UpdateAttackText()
    {
        _attackText.text = $"ATK:{AttackPower}";
    }
}
