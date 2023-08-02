using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour {
    [SerializeField] StringVariable message;
    private Text textUI;

    private void Start() {
        textUI = GetComponent<Text>();
        message.OnValueChanged += Message_OnValueChange;
    }

    private void Message_OnValueChange() {
        textUI.text = message.GetValue();
    }
}