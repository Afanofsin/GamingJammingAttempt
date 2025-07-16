using UnityEngine;

public class BuldingQuest : Quest.QuestGoal
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string Building;
    public override string GetDescription()
    {
        return "Collect " + 5 + " cards";
    }
    public override void Initialize()
    {
        base.Initialize();
    }
    private void OnBuilding(BuildingGameEvent eventInfo)
    {
        if (eventInfo.BuildingName == Building)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
