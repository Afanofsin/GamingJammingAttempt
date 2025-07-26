using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using System.Linq;

public class GameManagerSystem : MonoBehaviour
{
    public static GameManagerSystem Instance { get; private set; }
    public static event Action<GameState> OnSceneReady;

    public enum GameState { MainMenu, InHouse, InBattle }
    public GameState CurrentState { get; private set; }
    
    [SerializeField] private GameObject loadingScreenPrefab;
    private LoadingScreenUI _loadingScreenUI;

    [SerializeField] private GameObject MainMenuPrefab;
    private GameMainMenuUI _mainMenuUI;

    [SerializeField]
    private HeroDataSO _heroDataSO;
    [SerializeField]
    private Progression progression;
    [SerializeField]
    public GameObject houseTutor;

    [SerializeField]
    private List<CardDataSO> startingDeck;

    [SerializeField]
    private List<EnemyDataSO> listOfGameBosses;

    private Dictionary<string, EnemyDataSO> enemyLookupName;
    private List<EnemyDataSO> _enemiesForBattle = new();
    

    private void Start()
    {
        _heroDataSO.Reset();
        _heroDataSO.InitializeInventoryDeck(startingDeck);
        progression.ResetProgress();
        progression.Initialize(listOfGameBosses);

        enemyLookupName = progression.enemiesDefeated
            .Keys
            .ToDictionary( SO => SO.name.ToLowerInvariant(), SO => SO);

        _mainMenuUI.OpenMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _mainMenuUI.OpenOptions();
        }

    }

    public void GoToMainMenu()
    {
        StartCoroutine(LoadSceneCoroutine("MainMenu", GameState.MainMenu));
    }

    public void GoToHouseScene()
    {
        StartCoroutine(LoadSceneCoroutine("HomeScene", GameState.InHouse));
    }

    public void StartBattle(List<EnemyDataSO> enemies)
    {
        _enemiesForBattle = enemies;

        StartCoroutine(LoadSceneCoroutine("CardScene", GameState.InBattle));
    }

    public List<EnemyDataSO> GetEnemiesForBattle() { return _enemiesForBattle; }

    private IEnumerator LoadSceneCoroutine(string sceneName, GameState newState)
    {
        yield return StartCoroutine(_loadingScreenUI.FadeIn());

        CurrentState = newState;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Wait until the scene is fully loaded in the background
        while (asyncLoad.progress < 0.9f)
        {
            yield return null; 
        }

        // Optional: A minimum load time to ensure the player sees the animation
        yield return new WaitForSeconds(1.0f);

        asyncLoad.allowSceneActivation = true;

        // Wait for the new scene to be fully loaded and activated
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null;
        OnSceneReady?.Invoke(CurrentState);

        yield return StartCoroutine(_loadingScreenUI.FadeOut());
    }

    public void GameWon(int reward)
    {
        _mainMenuUI.WinScreenOpen(reward);
        _heroDataSO.Money += reward;
    }

    public void GameLost()
    {
        _mainMenuUI.OpenLoseScreen();
    }

    public void RestartLevel()
    {
        StartBattle(_enemiesForBattle);
    }

    public void Progress(string bossName)
    {
        string key = bossName.ToLowerInvariant();
        if (enemyLookupName.TryGetValue(key, out EnemyDataSO SO))
        {
            case "Sloth":
                //progression.isFirstBossKilled = true;
                break;
            case "Procrastination":
                //progression.isSecondBossKilled = true;
                break;
            case "ImposterSyndrome":
                //progression.isThirdBossKilled = true;
                break;
            case "FirstGame":
                //progression.isFourthBossKilled = true;
                break;
            default:
                break;
        }
        else
        {
            Debug.LogWarning("No boss with name " + bossName + "found");
        }

    }

    private void ResetGameState()
    {

    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        GameObject loadingScreenInstance = Instantiate(loadingScreenPrefab, transform);
        _loadingScreenUI = loadingScreenInstance.GetComponent<LoadingScreenUI>();
        GameObject mainMenuInstance = Instantiate(MainMenuPrefab, transform);
        _mainMenuUI = mainMenuInstance.GetComponent<GameMainMenuUI>();
        ResetGameState();
    }
}
