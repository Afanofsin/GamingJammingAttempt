using UnityEngine;

public class HeroView : CombatantView
{
    public Vector3 HeroPos;
    public void Setup(HeroDataSO heroDataSO)
    {
        HeroPos = this.transform.position;
        SetupBase(heroDataSO.Health, heroDataSO.Morale, heroDataSO.Image, heroDataSO.Controller);
    }
}
