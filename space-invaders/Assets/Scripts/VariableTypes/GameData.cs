using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public event Action OnGameOver;
    public event Action OnScoreChanged;
    public event Action OnPlayerKilled;

    [SerializeField] private int playerLives;
    [SerializeField] private int startingPlayerLives = 3;
    [SerializeField] private int score;
    [SerializeField] private bool gamePaused;
    [SerializeField] private bool gameOver;
    [SerializeField] private bool enemyMovementPaused;
    [SerializeField] private float playerSpeed;

    private int highScore;

    private void OnEnable() {
        ResetGame();
    }

    public void DecreasePlayerLives() {
        playerLives--;
        OnPlayerKilled?.Invoke();
        if (playerLives == 0 ) { 
            GameOver(); 
        }
    }

    public void ResetGame() {
        OnGameOver = null;
        OnScoreChanged = null;
        OnPlayerKilled = null;
        score = 0;
        gamePaused = false;
        gameOver = false;
        enemyMovementPaused = false;
        playerLives = startingPlayerLives;
        Wave.data.ResetGame();
    }
    
    public void PauseGame() {
        gamePaused = true;
    }

    public void UnpauseGame() {
        gamePaused = false;
    }

    public bool IsGamePaused() {
        if (gameOver) return true;
        else return gamePaused;
    }

    public void PauseEnemyMovement() {
        enemyMovementPaused = true;
    }
    
    public void UnpauseEnemyMovement() {
        enemyMovementPaused = false;
    }

    public bool IsEnemyMovementPaused() {
        if (IsGamePaused() || IsGameOver())
            return true;
        else
            return enemyMovementPaused;
    }

    public bool IsGameOver() {
        return gameOver;
    }

    public void GameOver() {
        PauseGame();
        gameOver = true;
        OnGameOver?.Invoke();
    }

    public void SetHighScore(int score) {
        highScore = score;
    }

    public int GetHighScore() => highScore;

    public float GetPlayerSpeed() {
        return playerSpeed;
    }

    public void SetScore(int value) {
        this.score = value;
        OnScoreChanged?.Invoke();
    }

    public void IncreaseScore(int increaseBy) {
        SetScore(score + increaseBy);
    }

    public int GetScore() => score;


}