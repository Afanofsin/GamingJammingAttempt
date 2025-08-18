using System.Collections;
using UnityEngine;

public class HeroView : CombatantView
{
    public Vector3 HeroPos;
    public float Money { get; private set; }

    public void Setup(HeroDataSO heroDataSO)
    {
        HeroPos = this.transform.position;
        SetupBase(heroDataSO.Health, heroDataSO.Morale, heroDataSO.Image, heroDataSO.Controller, heroDataSO.name);
        Money = heroDataSO.Money;
    }

    public IEnumerator DecreaseMoney(float amount)
    {
        Money -= amount;
        if (Money <= 0)
        {
            Money = 0;
            NoMoneyGA noMoneyGA = new();
            yield return MoneyUI.Instance?.UpdateMoney(Money);
            ActionSystem.Instance.AddReaction(noMoneyGA);
            yield break;
        }
        else
        {
            yield return MoneyUI.Instance?.UpdateMoney(Money);
        }
    }
}
