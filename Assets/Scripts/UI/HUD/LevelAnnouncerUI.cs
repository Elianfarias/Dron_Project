using Assets.Scripts.Gameplay.GameSystem;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.HUD
{
    public class LevelAnnouncerUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private float displayDuration = 2f;
        [SerializeField] private float fadeOutDuration = 0.5f;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            ShowLevelAnnouncement();
        }

        private void ShowLevelAnnouncement()
        {
            int level = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevel : 1;
            levelText.text = "Level " + level;

            panel.SetActive(true);
            canvasGroup.alpha = 1f;

            StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(displayDuration);

            float elapsed = 0f;
            while (elapsed < fadeOutDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = 1f - (elapsed / fadeOutDuration);
                yield return null;
            }

            panel.SetActive(false);
        }
    }
}