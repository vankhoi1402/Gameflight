using System;
using MyGame.UI.Core;
using MyGame.UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
public enum GameState
{
    Playing,
    Win,
    Lose
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set ; }
    public float currentScore  { get; private set ; }
    public float time { get; private set ; }
    public GameState State { get; private set; } = GameState.Playing;
    public LoseReason CurrentLoseReason { get; private set; } = LoseReason.None;
    public event Action<GameState, LoseReason> OnGameStateChanged;


    private void Awake()
    {
        // Thiết lập Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // tránh trùng
        currentScore = 0;

        
    }
    private void Update()
    {
        time += Time.deltaTime;
    }

    public float addScore(float score)
    {
       return  currentScore += score;
    }
    private void ShowStatust()
    {
        
    }
    public void SetState(GameState newState, LoseReason reason = LoseReason.None)
    {
        Debug.Log($"{newState}");
        if (State == newState) return;

        State = newState;
        CurrentLoseReason = reason;

        OnGameStateChanged?.Invoke(State, CurrentLoseReason);
    }

    public void EndGame(GameState gameState) {
        SetState(gameState);
        // Lấy GameOverUI đã được khai báo trong UIManager
        Debug.Log("UIManager.Instance = " + UIManager.Instance);
        Debug.Log("UIManager.Instance.GameOverUI = " + UIManager.Instance?.GameOverUI);

        var gameOverScreen = UIManager.Instance.GameOverUI;
        gameOverScreen.Setup(gameState, currentScore);

        // Hiển thị GameOver screen
        UIManager.Instance.ShowScreen(gameOverScreen);
        if ( gameState==GameState.Win) LevelManager.Instance.LevelComplete();
       // StartCoroutine(PauseCoroutine());


    }
    public void LoseGame(LoseReason reason) => SetState(GameState.Lose, reason);
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLv()
    {
        Time.timeScale = 1f;
        
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator PauseCoroutine()
    {
        yield return new WaitForSeconds(1); // chờ delay giây
        Time.timeScale = 0f;
        Debug.Log("Game đã tạm dừng!");
    }
    public void ResetGameData()
    {
        currentScore= 0;
        Debug.Log("Dữ liệu game đã reset!");
    }


}
