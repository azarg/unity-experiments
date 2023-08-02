using System;
using UnityEngine;

[CreateAssetMenu]
public class HandVariable : ScriptableObject {

    public event Action OnValueChanged;
    
    [SerializeField] private Hand value;

    public void SetValue(Hand _value) {
        if (value != _value) {
            value = _value;
            OnValueChanged?.Invoke();
        }
    }

    public Hand GetValue() { return value; }

    public bool HasSprite() {
        return value != null && value.sprite != null;
    }

    private void OnEnable() {
        value = null;
    }
}