using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BattleApp : MonoBehaviour
{
    [SerializeField] private List<EnemyDataSO> bosses;
    public HeroDataSO heroDataSO;
    public Progression progression;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Button startBattleButton;
    public GameObject notEnoughCardsText;
    public Image bossImage;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI bossDescription;
    public List <EnemyDataSO> bossData;
    void OnEnable()
    {
        bossData.Clear();
        bossName.text = null;
        bossDescription.text = null;
        bossImage.enabled = false;
        notEnoughCardsText.SetActive(false);
        startBattleButton.gameObject.SetActive(false);
        EvaluateBosses();
    }

    public void EvaluateBosses()
    {
    

    }
    public void ShowBossButtons()
    {
        foreach (var boss in bosses)
        {
            Button newButtton = Instantiate(buttonPrefab, buttonPrefab.transform.position, quaternion.identity); 
        }
    }
     public void StartBattle()
    {
        if (heroDataSO.Deck.Count < 10)
        {
            notEnoughCardsText.SetActive(true);
            return;
        }
        GameManagerSystem.Instance.StartBattle(bossData);
    }
    public void ShowBoss(int i)
    {
        bossData = new List<EnemyDataSO>() { bosses[i] };
        bossName.text = bosses[i].name;
        bossImage.sprite = bosses[i].Image;
        bossImage.enabled = true;
        startBattleButton.gameObject.SetActive(true);
        bossDescription.text = bosses[i].Description;
    }
    public void SwtichBattleApp()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
