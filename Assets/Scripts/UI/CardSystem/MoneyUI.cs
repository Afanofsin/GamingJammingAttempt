using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public static MoneyUI Instance;
    [SerializeField] Image backdrop;
    [SerializeField] TMP_Text moneyText;

    public void Setup(float money)
    {
        moneyText.text = $"{money}$";
    }

    public IEnumerator UpdateMoney(float money)
    {
        Tween tween = this.transform.DOLocalMoveY(180, 0.2f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(0.15f);
        tween = this.transform.DOShakePosition(1f, 3f, 20);
        moneyText.text = $"{money}$";
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(0.15f);
        tween = this.transform.DOLocalMoveY(206, 0.2f);
        yield return tween.WaitForCompletion();
    } 

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
