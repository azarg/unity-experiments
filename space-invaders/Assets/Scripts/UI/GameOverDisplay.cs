using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour {
    [SerializeField] private GameObject gameOverText;

    private void Start() {
        Game.data.OnGameOver += OnGameOver;
    }

    private void OnGameOver() {
        Debug.Log("OnGameOver in GameOverDisplay");
        if (gameOverText != null) {
            gameOverText.SetActive(true);
        }
        else {
            Debug.Log("game over text is null");
        }
    }
}