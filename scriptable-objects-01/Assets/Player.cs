using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FloatVariable health;
    public int maxHealth = 10;

    private void Start() {
        health.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            health.value--;
            if (health.value < 0) { health.value = 0; }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            health.value++;
            if (health.value > maxHealth) { health.value = maxHealth; }
        }

    }
}
