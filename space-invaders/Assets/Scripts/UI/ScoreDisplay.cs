using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
    [SerializeField] private Text scoreText;

    private void Start () {
        GameManager.Instance.gameData.OnScoreChanged += Score_OnValueChanged;
    }

    private void Score_OnValueChanged() {
        scoreText.text = GameManager.Instance.gameData.GetScore().ToString("000");
    }
}
