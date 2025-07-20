using UnityEngine;

public class DeckerCardShow : MonoBehaviour
{
    public void ShowCard()
    {
    EventManager.Instance.AddCardToDeckBuilder();
 }
}
