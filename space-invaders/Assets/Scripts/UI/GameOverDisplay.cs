using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour {
    [SerializeField] private GameObject gameOverText;

    private void Start() {
        GameManager.Instance.gameData.OnGameOver += OnGameOver;
    }

    private void OnGameOver() {
        if (gameOverText != null) {
            gameOverText.SetActive(true);
        }
    }
}