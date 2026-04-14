using UnityEngine;

namespace Assets.Scripts.Gameplay.GameSystem
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public int CurrentLevel { get; private set; } = 1;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AdvanceLevel()
        {
            CurrentLevel++;
        }

        public void ResetLevels()
        {
            CurrentLevel = 1;
        }

        public void SetLevel(int level)
        {
            CurrentLevel = Mathf.Max(1, level);
        }
    }
}
