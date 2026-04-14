using Assets.Scripts.Gameplay.GameSystem;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text txtCivil;
    [SerializeField] private TMP_Text txtEnemy;
    [SerializeField] private LevelConfigSO levelConfig;

    public int civilMaxCount;
    public int civilCount;
    public int enemyMaxCount;
    public int enemyCount;
    public int civilSaved = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        int level = GetCurrentLevel();

        civilMaxCount = levelConfig.GetCivilCount(level);
        civilCount = civilMaxCount;
    }

    private void Start()
    {
        txtCivil.text = civilCount + "/" + civilMaxCount;
    }

    private int GetCurrentLevel()
    {
        if (LevelManager.Instance == null)
            return 1;

        return LevelManager.Instance.CurrentLevel;
    }

    public void LessCivilScore()
    {
        civilCount--;
        txtCivil.text = civilCount + "/" + civilMaxCount;

        if (civilCount <= 0)
            GameStateManager.Instance.SetGameState(GameState.GAME_OVER);
    }

    public void LessEnemyScore()
    {
        enemyCount--;
        txtEnemy.text = enemyCount + "/" + enemyMaxCount;

        if (enemyCount <= 0)
            GameStateManager.Instance.SetGameState(GameState.WIN);
    }

    public void AddCivilSavedScore()
    {
        civilSaved++;
        if (civilSaved >= civilCount)
            GameStateManager.Instance.SetGameState(GameState.WIN);
    }

    public void SetEnemyCount(int count)
    {
        enemyMaxCount = count;
        enemyCount = count;
        txtEnemy.text = enemyCount + "/" + enemyMaxCount;
    }
}