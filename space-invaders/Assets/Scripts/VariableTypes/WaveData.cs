using System;
using UnityEngine;

public enum Direction { RIGHT, LEFT }

[CreateAssetMenu]
public class WaveData : ScriptableObject {

    public event Action OnNoEnemiesLeft;

    [SerializeField] private float speed;
    [SerializeField] private float animationSpeed;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float animationSpeedIncrement;
    [SerializeField] private Direction direction;

    [SerializeField] private float startingSpeed = 2f;
    [SerializeField] private float startingSpeedIncrement = 0.1f;
    [SerializeField] private float startingAnimationSpeed = 1f;
    [SerializeField] private float startingAnimationSpeedIncrement = 0.2f;
    [SerializeField] private int enemyCount;

    private void OnEnable() {
        speed = startingSpeed;
        speedIncrement = startingSpeedIncrement;
        animationSpeed = startingAnimationSpeed;
        animationSpeedIncrement = startingAnimationSpeedIncrement;
        direction = Direction.RIGHT;
        enemyCount = 0;
    }
    public void Freeze() {
        speedIncrement = 0;
        speed = 0;
        animationSpeed = 0;
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
        return speed;
    }

    public void IncreaseWaveSpeed() {
        speed += speedIncrement;
        animationSpeed += animationSpeedIncrement;
    }

    public Direction GetDirection() {
        return direction;
    }

    public void SetDirection(Direction direction) {
        this.direction = direction;
    }

    internal float GetAnimationSpeed() {
        return animationSpeed;
    }
}
