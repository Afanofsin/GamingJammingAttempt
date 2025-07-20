using DG.Tweening;
using System.Collections;
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
    public Animator Animator;
    [SerializeField]
    private Transform barsParent;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private StatusEffectsUI statusEffectsUI;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }
    public int CurrentMorale { get; private set; }

    private Dictionary<StatusEffectType, int> statusEffects = new();
    private Tween _jiggleTween;

    protected void SetupBase(int health, int morale, Sprite image, RuntimeAnimatorController controller)
    {
        MaxHealth = CurrentHealth = health;
        if (morale != 0) CurrentMorale = morale;
        else CurrentMorale = 0;
        _spriteRenderer.sprite = image;
        Animator.runtimeAnimatorController = controller;

        UpdateHealthText();
        ManageMoraleSprite();
        UpdateMoraleText();
        StartJiggling();
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

    private void StartJiggling()
    {
        StopJiggling();

        Vector3 originalPos = barsParent.transform.position;
        _jiggleTween = barsParent.transform
            .DOMoveY(originalPos.y + 0.0625f, 1.15f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    private void StopJiggling()
    {
        _jiggleTween?.Kill();
    }

    public void Damage(int damageAmount)
    {
        if (GetStatusEffectStacks(StatusEffectType.PROCRASTINATION) > 0) return;
        int remainingDamage = damageAmount;
        int currentShield = GetStatusEffectStacks(StatusEffectType.SHIELD);

        if (currentShield != 0) 
        {
            RemoveStatusEffect(StatusEffectType.SHIELD, 1);
            remainingDamage = 0;
        }

        remainingDamage = DamageMorale(remainingDamage);

        DamageHealth(remainingDamage);

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
        UpdateMoraleText();
        ManageMoraleSprite();
    }

    public void DirectDamage(int damageAmount, DirectType type)
    {
        if (GetStatusEffectStacks(StatusEffectType.PROCRASTINATION) > 0) return;
        int remainingDamage = damageAmount;
        switch (type)
        {
            case DirectType.IGNORESHIELD:
                remainingDamage = DamageMorale(remainingDamage);
                DamageHealth(remainingDamage);
                break;
            case DirectType.MORALE:
                DamageMorale(remainingDamage);
                break;
            case DirectType.HEALTH:
                DamageHealth(remainingDamage);
                break;
        }
        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
        UpdateMoraleText();
        ManageMoraleSprite();
    }

    private int DamageMorale(int damage)
    {
        int remainingDamage = damage;
        if (CurrentMorale > 0)
        {
            if (CurrentMorale >= remainingDamage)
            {
                CurrentMorale -= remainingDamage;
                remainingDamage = 0;
            }
            else if (CurrentMorale < remainingDamage)
            {
                remainingDamage -= CurrentMorale;
                CurrentMorale = 0;
            }
        }
        return remainingDamage;
    }

    private void DamageHealth(int remainingDamage)
    {
        if (remainingDamage > 0)
        {
            CurrentHealth -= remainingDamage;
            if (CurrentHealth < 0) CurrentHealth = 0;
        }
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
