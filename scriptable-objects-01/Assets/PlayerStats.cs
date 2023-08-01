using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject {
    [Range(0,10)]
    public float health;
    private readonly float maxHealth = 10;
    private readonly float minHealth = 0;

    private void OnEnable() {
        health = maxHealth;
    }
    public void ChangeHealth(float amount) {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        if (health < minHealth) health = minHealth;
    }
}