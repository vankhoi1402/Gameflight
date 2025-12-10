using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyGame.UI.Core;
using Unity.Mathematics;
using System;

namespace MyGame.UI.Screens
{
    public class GameOverUI : UIScreen
    {
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button nextLvButton;

        public void Setup(GameState gameState, float score)
        {
            // Cập nhật text kết quả và điểm số như cũ
            resultText.text = gameState == GameState.Win ? "YOU WIN!" : "GAME OVER";
            scoreText.text = $"Score: {math.round(score)}";

            // --- Logic mới để hiển thị/ẩn nút ---

            bool isWin = gameState == GameState.Win;

            // Khi thắng: Bật nút Tiếp theo, Tắt nút Chơi lại
            nextLvButton.gameObject.SetActive(isWin);
            // Khi thua: Bật nút Chơi lại, Tắt nút Tiếp theo
            retryButton.gameObject.SetActive(!isWin);
        }

        public override void Show()
        {
            base.Show();

            retryButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            nextLvButton.onClick.RemoveAllListeners();
            

            retryButton.onClick.AddListener(() => LevelManager.Instance.ReloadCurrentLevel());
            exitButton.onClick.AddListener(() => GameManager.Instance.ExitToMenu());
            nextLvButton.onClick.AddListener(() => LevelManager.Instance.NextLevel());

        }
    }
}

