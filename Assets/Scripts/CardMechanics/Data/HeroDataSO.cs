using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Hero")]
public class HeroDataSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite Image {  get; private set; }
    [field: SerializeField]
    public int Health { get; private set; }
    [field: SerializeField]
    public int Morale { get; private set; }
    [field: SerializeField]
    public List<CardDataSO> Deck {  get; private set; }
}
