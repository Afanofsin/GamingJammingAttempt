using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class BattleApp : MonoBehaviour
{
    [SerializeField] private List<EnemyDataSO> bosses;
    [SerializeField] private HeroDataSO heroDataSO;
    [SerializeField] private Progression progression;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Button startBattleButton;
    [SerializeField] private GameObject notEnoughCardsText;
    [SerializeField] private Image bossImage;
    [SerializeField] private Sprite lockedImage;
    [SerializeField] private TextMeshProUGUI bossName;
    [SerializeField] private TextMeshProUGUI bossDescription;
    [SerializeField] private List<EnemyDataSO> bossData;
    private List<Button> buttonsToDestroy = new();
    void OnEnable()
    {
        DestroyAllBossButtons();
        ShowBossButtons();
        bossData.Clear();
        bossName.text = null;
        bossDescription.text = null;
        bossImage.enabled = false;
        notEnoughCardsText.SetActive(false);
        startBattleButton.gameObject.SetActive(false);
    }
    public void ShowBossButtons()
    {
        int index = 0;
        foreach (var boss in bosses)
        {
            int helpIndex = index;
            var newButton = Instantiate(buttonPrefab, buttonPrefab.transform.position, quaternion.identity);
            newButton.transform.SetParent(GameObject.Find("ButtonHelper").transform);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            if (helpIndex == 0)
            {
                newButton.enabled = true;
                newButton.image.sprite = boss.BattleAppImage;
            }
            else if (helpIndex >= 1 && !progression.enemiesDefeated[bosses[helpIndex - 1]])
            {
                newButton.image.sprite = lockedImage;
                newButton.enabled = false;
            }
            else
            {
                newButton.enabled = true;
                newButton.image.sprite = boss.BattleAppImage;
            }
            newButton.onClick.AddListener(() => ShowBoss(helpIndex));
            buttonsToDestroy.Add(newButton);
            index++;
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
    private void DestroyAllBossButtons()
    {
        foreach (var button in buttonsToDestroy)
        {
            if (button != null)
            {
                Destroy(button.gameObject);
            }
        }
        buttonsToDestroy.Clear();
    }
    void OnDisable()
    {
        DestroyAllBossButtons();
    }

}
