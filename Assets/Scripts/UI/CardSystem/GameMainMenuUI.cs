using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using static GameManagerSystem;

public class GameMainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _inGameMenu;
    [SerializeField]
    private GameObject _inGameHouse;
    [SerializeField]
    private GameObject _WinMenu;
    [SerializeField] 
    private TMP_Text _WinMenuText;
    [SerializeField]
    private GameObject _LoseMenu;
    [SerializeField]
    private bool wasTutorActive = false;
    private GameObject tutorialObject;
    private GameState state;
    private bool isOptionsOpens = false;
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
        state = gameState;
        if(gameState != GameState.MainMenu)
        {
            _mainMenu.SetActive(false);
            _inGameMenu.SetActive(false);
            _WinMenu.SetActive(false);
            _LoseMenu.SetActive(false);
            if (gameState == GameState.InHouse)
            {
                ShowTutorial();
                if (tutorialObject == null)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        else
        {
            _mainMenu.SetActive(true);
            _inGameMenu.SetActive(false);
            _WinMenu.SetActive(false);
            _LoseMenu.SetActive(false);
        }
    }

    public void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
        _inGameMenu.SetActive(false);
        _WinMenu.SetActive(false);
        _LoseMenu.SetActive(false);
    }
    public void WinScreenOpen(int reward)
    {
        _mainMenu.SetActive(false);
        _inGameMenu.SetActive(false);
        _LoseMenu.SetActive(false);
        _WinMenu.SetActive(true);
        _WinMenuText.text = $"You won:\r\n|{reward}$|\r\nSpend them on some new cards!\r\n";
    }

    public void OpenLoseScreen()
    {
        _mainMenu.SetActive(false);
        _inGameMenu.SetActive(false);
        _LoseMenu.SetActive(true);
        _WinMenu.SetActive(false);
    }

    public void ShowTutorial()
    {
        if (wasTutorActive) return;
        tutorialObject = Instantiate(GameManagerSystem.Instance.houseTutor, transform.position, Quaternion.identity);
        wasTutorActive = true;
    }

    public void RestartLevel()
    {
        GameManagerSystem.Instance?.RestartLevel();
    }
    public void GoBackToHouse()
    {
        GameManagerSystem.Instance?.GoToHouseScene();
    }

    public void GoToMainMenu()
    {
        GameManagerSystem.Instance?.GoToMainMenu();
    }

    public void OpenOptions()
    {
        if (state == GameState.MainMenu) return;
        if (isOptionsOpens)
        {
            CloseOptions();
            return;
        }
        isOptionsOpens = true;
        _mainMenu.SetActive(false);
        _inGameMenu.SetActive(true);
        _WinMenu.SetActive(false);

        if(state == GameState.InHouse)
        {
            _inGameHouse.GetComponent<Button>().enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseOptions()
    {
        if (state == GameState.MainMenu) return;
        isOptionsOpens = false;
        _mainMenu.SetActive(false);
        _inGameHouse.GetComponent<Button>().enabled = true;
        _inGameMenu.SetActive(false);
        _WinMenu.SetActive(false);

        if (state == GameState.InHouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
           
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
