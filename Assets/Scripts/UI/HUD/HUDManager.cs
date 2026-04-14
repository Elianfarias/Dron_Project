using Assets.Scripts.Gameplay.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("Player Lose HUD")]
    [SerializeField] private GameObject panelPlayerLose;
    [SerializeField] private Button btnReset;
    [SerializeField] private Button btnBackToMenu;

    [Header("Player Win HUD")]
    [SerializeField] private GameObject panelPlayerWin;
    [SerializeField] private TextMeshProUGUI descriptionWin;
    [SerializeField] private Button btnNextLevel;
    [SerializeField] private Button btnWinBackToMenu;

    private void Awake()
    {
        Instance = this;

        if (btnReset != null)
        {
            btnReset.onClick.AddListener(RestartLevel);
            btnBackToMenu.onClick.AddListener(BackToMenu);
        }

        if (btnNextLevel != null)
        {
            btnNextLevel.onClick.AddListener(GoToNextLevel);
            btnWinBackToMenu.onClick.AddListener(BackToMenu);
        }
    }

    private void OnDestroy()
    {
        if (btnReset != null)
        {
            btnReset.onClick.RemoveAllListeners();
            btnBackToMenu.onClick.RemoveAllListeners();
        }

        if (btnNextLevel != null)
        {
            btnNextLevel.onClick.RemoveAllListeners();
            btnWinBackToMenu.onClick.RemoveAllListeners();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SkipToLevel(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SkipToLevel(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SkipToLevel(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SkipToLevel(4);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) SkipToLevel(5);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) SkipToLevel(6);
        else if (Input.GetKeyDown(KeyCode.Alpha7)) SkipToLevel(7);
        else if (Input.GetKeyDown(KeyCode.Alpha8)) SkipToLevel(8);
        else if (Input.GetKeyDown(KeyCode.Alpha9)) SkipToLevel(9);
    }

    private void SkipToLevel(int level)
    {
        if (LevelManager.Instance == null) return;

        Time.timeScale = 1;
        LevelManager.Instance.SetLevel(level);
        GameStateManager.Instance.SetGameState(GameState.PLAYING);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPanelPlayerLose()
    {
        panelPlayerLose.SetActive(true);
    }

    public void ShowPanelPlayerWin()
    {
        int level = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevel : 1;
        descriptionWin.text = "Level " + level + " Complete!\n" +
                              "Rescued " + ScoreManager.Instance.civilCount + " civilians";
        panelPlayerWin.SetActive(true);
    }

    private void BackToMenu()
    {
        Time.timeScale = 1;
        if (LevelManager.Instance != null)
            LevelManager.Instance.ResetLevels();
        SceneManager.LoadScene("MainMenu");
    }

    private void RestartLevel()
    {
        Time.timeScale = 1;
        GameStateManager.Instance.SetGameState(GameState.PLAYING);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToNextLevel()
    {
        Time.timeScale = 1;
        if (LevelManager.Instance != null)
            LevelManager.Instance.AdvanceLevel();
        GameStateManager.Instance.SetGameState(GameState.PLAYING);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}