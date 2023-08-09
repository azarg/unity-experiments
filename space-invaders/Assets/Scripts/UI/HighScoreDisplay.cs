using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class HighScoreDisplay : MonoBehaviour {
    private Text display;

    private void Start() {
        display = GetComponent<Text>();
        display.text = Game.data.GetHighScore().ToString("0000");
    }
}