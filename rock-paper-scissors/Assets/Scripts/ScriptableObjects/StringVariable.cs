using System;
using UnityEngine;

[CreateAssetMenu]
public class StringVariable : ScriptableObject {

    public event Action OnValueChanged;

    [SerializeField] private string value;

    public void SetValue(string _value) {
        value = _value;
        OnValueChanged?.Invoke();
    }

    public string GetValue() { return value; }

    private void OnEnable() {
        value = string.Empty;
    }
}