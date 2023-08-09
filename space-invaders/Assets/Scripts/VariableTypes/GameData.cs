using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public event Action OnGameOver;
    public event Action OnScoreChanged;

    [SerializeField] private int score;
    [SerializeField] private bool gameOver;
    [SerializeField] private float playerSpeed;
    private int highScore;

    private void OnEnable() {
        score = 0;
        gameOver = false;
    }

    public void ResetGame() {
        score = 0;
        gameOver = false;
    }

    public bool IsGameOver() {
        return gameOver;
    }

    public void GameOver() {
        gameOver = true;
        OnGameOver?.Invoke();
    }

    public void SetHighScore(int score) {
        highScore = score;
    }

    public int GetHighScore() => highScore;

    public float GetPlayerSpeed() {
        if (IsGameOver())
            return 0;
        else
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