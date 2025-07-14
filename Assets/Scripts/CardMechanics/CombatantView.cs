using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        _spriteRenderer.sprite = image;
        UpdateHealthText();
    }
    private void UpdateHealthText()
    {
        _healthText.text = $"{CurrentHealth}/{MaxHealth}";
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth < 0) CurrentHealth = 0;

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }
}
