using System;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject {
    
    public event Action OnValueChanged;

    [SerializeField] private int value;

    public void SetValue(int value) {
        this.value = value;
        OnValueChanged?.Invoke();
    }

    public int GetValue() => value;

    private void OnEnable() {
        value = 0;
    }
}
