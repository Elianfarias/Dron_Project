using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [Header("Base Settings")]
    public int baseEnemyCount = 3;
    public int baseCivilCount = 4;

    [Header("Scaling")]
    public int enemiesPerTier = 1;
    public int levelsPerTier = 2;

    public int GetEnemyCount(int level)
    {
        int tier = (level - 1) / levelsPerTier;
        return baseEnemyCount + tier * enemiesPerTier;
    }

    public int GetCivilCount(int level)
    {
        return baseCivilCount;
    }
}