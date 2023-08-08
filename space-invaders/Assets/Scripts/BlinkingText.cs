using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {
    private Text text;
    private void Start() {
        text = gameObject.GetComponent<Text>();
        InvokeRepeating(nameof(Blink), 1, 1);
    }
    private void Blink() {
        var a = text.color.a;
        a = a == 0 ? 1 : 0;
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
    }
}