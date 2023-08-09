using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start () {
        Game.data.OnScoreChanged += Score_OnValueChanged;
    }

    private void Score_OnValueChanged() {
        scoreText.text = Game.data.GetScore().ToString("000");
    }
}
