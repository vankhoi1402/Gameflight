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
            resultText.text = gameState==GameState.Win ? "YOU WIN!" : "GAME OVER";
            scoreText.text = $"Score: {math.round( score)}";
        }

        public override void Show()
        {
            base.Show();

            retryButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            nextLvButton.onClick.RemoveAllListeners();
            

            retryButton.onClick.AddListener(() => GameManager.Instance.RetryLevel());
            exitButton.onClick.AddListener(() => GameManager.Instance.ExitToMenu());
            nextLvButton.onClick.AddListener(() => LevelManager.Instance.NextLevel());

        }
    }
}

