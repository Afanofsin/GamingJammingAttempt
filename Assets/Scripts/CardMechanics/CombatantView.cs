using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Transform _healthMask;
    [SerializeField]
    private TMP_Text _moraleText;
    [SerializeField]
    private SpriteRenderer _moraleSpriteRenderer;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private StatusEffectsUI statusEffectsUI;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }
    public int CurrentMorale { get; private set; }

    private Dictionary<StatusEffectType, int> statusEffects = new();

    protected void SetupBase(int health, int morale, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        if (morale != 0) CurrentMorale = morale;
        else CurrentMorale = 0;
        _spriteRenderer.sprite = image;
        UpdateHealthText();

        ManageMoraleSprite();
        UpdateMoraleText();
    }
    private void UpdateHealthText()
    {
        _healthText.text = $"{CurrentHealth}/{MaxHealth}";
        float hpRatio = (float)CurrentHealth / (float)MaxHealth  * 3f;
        _healthMask.transform.localScale = new Vector3(hpRatio, 0.625f, 0.01f);
    }

    private void UpdateMoraleText()
    {
        _moraleText.text = $"{CurrentMorale}";
        ManageMoraleSprite();
    }

    private void ManageMoraleSprite()
    {
        if (CurrentMorale > 0) _moraleSpriteRenderer.enabled = true;
        else
        {
            _moraleSpriteRenderer.enabled = false;
            _moraleText.text = "";
        }
    }

    public void Damage(int damageAmount)
    {
        int remainingDamage = damageAmount;
        int currentShield = GetStatusEffectStacks(StatusEffectType.SHIELD);

        if (currentShield != 0) 
        {
            RemoveStatusEffect(StatusEffectType.SHIELD, 1);
            remainingDamage = 0;
        }

        if(CurrentMorale > 0)
        {
            if(CurrentMorale >= remainingDamage)
            {
                CurrentMorale -= remainingDamage;
                remainingDamage = 0;
            }
            else if(CurrentMorale < remainingDamage) 
            {
                remainingDamage -= CurrentMorale;
                CurrentMorale = 0;
            }
        }

        if (remainingDamage > 0) 
        {
            CurrentHealth -= remainingDamage;
            if (CurrentHealth < 0) CurrentHealth = 0;
        }

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
        UpdateMoraleText();
        ManageMoraleSprite();
    }

    public void AddStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] += stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }

    public void RemoveStatusEffect(StatusEffectType type, int stackCount) 
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] -= stackCount;
            if (statusEffects[type] <= 0)
            {
                statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));

    }
    
    public void AddMorale(int amount)
    {
        CurrentMorale += amount;
        UpdateMoraleText();
        ManageMoraleSprite();
    }

    public int GetStatusEffectStacks(StatusEffectType type)
    {
        if(statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }
}
