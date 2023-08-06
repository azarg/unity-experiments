using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
    [SerializeField] IntVariable score;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start () {
        score.OnValueChanged += Score_OnValueChanged;
    }

    private void Score_OnValueChanged() {
        scoreText.text = score.GetValue().ToString("000");
    }
}
