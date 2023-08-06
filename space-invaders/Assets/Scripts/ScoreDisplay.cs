using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
    [SerializeField] IntVariable score;
    [SerializeField] private Text scoreText;

    private void Start () {
        score.OnValueChanged += Score_OnValueChanged;
    }

    private void Score_OnValueChanged() {
        scoreText.text = $"score:" + score.GetValue().ToString("000");
    }
}
