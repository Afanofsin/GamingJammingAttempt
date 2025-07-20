using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Android;
using static GameManagerSystem;

public class GameMainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _inGameMenu;
    [SerializeField]
    private GameObject _WinMenu;
    [SerializeField] 
    private TMP_Text _WinMenuText;
    [SerializeField]
    private GameObject _LoseMenu;

    private void OnEnable()
    {
        GameManagerSystem.OnSceneReady += SceneSetup;
    }

    private void OnDisable() 
    {
        GameManagerSystem.OnSceneReady -= SceneSetup;
    }

    private void SceneSetup(GameState gameState)
    {
        if(gameState != GameState.MainMenu)
        {
            _mainMenu.SetActive(false);
            _inGameMenu.SetActive(false);
            _WinMenu.SetActive(false);
            _LoseMenu.SetActive(false);
        }
        else
        {
            _mainMenu.SetActive(true);
            _inGameMenu.SetActive(false);
            _WinMenu.SetActive(false);
            _LoseMenu.SetActive(false);
        }
    }
    public void WinScreenOpen(int reward)
    {
        _mainMenu.SetActive(false);
        _inGameMenu.SetActive(false);
        _LoseMenu.SetActive(false);
        _WinMenu.SetActive(true);
        _WinMenuText.text = $"You won:\r\n|{reward}$|\r\nSpend them on some new cards!\r\n";
    }

    public void GoBackToHouse()
    {
        GameManagerSystem.Instance.GoToHouseScene();
    }
}
