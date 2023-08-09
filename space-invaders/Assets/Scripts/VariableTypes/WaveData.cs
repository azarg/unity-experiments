using System;
using UnityEngine;

public enum Direction { RIGHT, LEFT }

[CreateAssetMenu]
public class WaveData : ScriptableObject {

    public event Action OnNoEnemiesLeft;

    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentAnimationSpeed;

    [SerializeField] private float startingSpeed = 0.3f;
    [SerializeField] private float startingAnimationSpeed = 0.6f;

    [SerializeField] private float speedIncrement = 0.05f;
    [SerializeField] private float animationSpeedIncrement = 0.05f;
    [SerializeField] private float waveSpeedIncrement = 0.2f;

    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float fireChance = 0.5f;

    [SerializeField] private int enemyCount;
    [SerializeField] private Direction direction;

    private void OnEnable() {
        currentSpeed = startingSpeed;
        currentAnimationSpeed = startingAnimationSpeed;
        direction = Direction.RIGHT;
        enemyCount = 0;
    }

    public void AddEnemy() {
        enemyCount++;
    }

    public void RemoveEnemy() {
        if (enemyCount == 0) Debug.LogError("enemy count is already zero");
        enemyCount--;
        if (enemyCount == 0) {
            OnNoEnemiesLeft?.Invoke();
        }
    }

    public float GetWaveSpeed() {
        return currentSpeed;
    }

    public void ResetSpeedForWave(int waveNumber) {
        currentSpeed = startingSpeed + waveNumber * waveSpeedIncrement;
        currentAnimationSpeed = startingAnimationSpeed + waveNumber * waveSpeedIncrement;
    }

    public void IncreaseWaveSpeed() {
        currentSpeed += speedIncrement;
        currentAnimationSpeed += animationSpeedIncrement;
    }

    public float GetWaveSpeedIncrement() {
        return waveSpeedIncrement;
    }

    public Direction GetDirection() {
        return direction;
    }

    public void SetDirection(Direction direction) {
        this.direction = direction;
    }

    public float GetAnimationSpeed() {
        return currentAnimationSpeed;
    }

    public float GetFireRate() {
        return fireRate;
    }

    public float GetFireChance() {
        return fireChance;
    }
}
