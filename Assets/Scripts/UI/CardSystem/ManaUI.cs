using UnityEngine;
using TMPro;

public class ManaUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _manaText;

    public void UpdateManaText(int currentMana, int MAX_MANA)
    {
        _manaText.text = $"{currentMana} {MAX_MANA}";
    }
}
