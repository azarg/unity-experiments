using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesDisplay : MonoBehaviour {

    [SerializeField] private Image[] lives;

    private void Start() {
        GameManager.Instance.gameData.OnPlayerKilled += OnPlayerKilled;
    }

    private void OnPlayerKilled() {
        for(int i = 0; i < lives.Length; i++) {
            if (lives[i] != null && lives[i].gameObject.activeSelf) {
                lives[i].gameObject.SetActive(false);
                break;
            }
        }
    }
}
