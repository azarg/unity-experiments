using UnityEngine;

public enum Direction { RIGHT, LEFT }

[CreateAssetMenu]
public class WaveData : ScriptableObject {
    [SerializeField] private float startingSpeed = 2f;
    [SerializeField] private float speed;
    [SerializeField] private Direction direction;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float moveDownAmount;

    private void OnEnable() {
        speed = startingSpeed;
        direction = Direction.RIGHT;
    }

    public float GetWaveSpeed() {
        return speed;
    }

    public void IncreaseWaveSpeed() {
        speed += speedIncrement;
    }

    public Direction GetDirection() {
        return direction;
    }

    public void ChangeDirection() {
        if (direction == Direction.LEFT) {
            direction = Direction.RIGHT;
        } else {
            direction = Direction.LEFT;
        }
    }

    public float GetMoveDownAmount() {
        return moveDownAmount;
    }
}
