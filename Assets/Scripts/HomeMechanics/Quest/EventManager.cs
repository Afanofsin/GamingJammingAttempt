using System;
using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private IEnumerator CardEventPerformer(CollectCardEvent collectCardEvent)
    {
        yield return null;
    }
    public void OnCLick()
    {
        CollectCardEvent collectCardEvent = new();
        ActionSystem.Instance.Perform(collectCardEvent);
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
