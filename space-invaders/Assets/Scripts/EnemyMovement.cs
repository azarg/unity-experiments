using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] WaveData waveData;
    [SerializeField] BoundsVariable bounds;

    // need to remember the last direction, because direction may be changed
    // by other enemies. and if it changes, then I need to move down and
    // remember the new direction.
    private Direction lastDirection;

    private void Update() {
        Direction currentDirection = waveData.GetDirection();
        if (currentDirection != lastDirection) {
            // direction has changed (meaning one of the enemies has hit the bounds)
            // move down and remember the current direction
            MoveDown();
            lastDirection = currentDirection;
        }
        else {
            // direction hasn't changed from last frame, continue moving
            // and change direction if bounds have been hit 
            MoveInDirection(currentDirection);
            ChangeDirectionIfBoundsHit(currentDirection);
        }
    }

    private void MoveInDirection(Direction direction) {
        var moveVector = direction == Direction.RIGHT ? Vector3.right : Vector3.left;
        transform.Translate(waveData.GetWaveSpeed() * Time.deltaTime * moveVector);
    }

    private void ChangeDirectionIfBoundsHit(Direction direction) {
        if (direction == Direction.RIGHT) {
            if (transform.position.x > bounds.right) {
                waveData.ChangeDirection();
            }
        }
        if (direction == Direction.LEFT) {
            if (transform.position.x < bounds.left) {
                waveData.ChangeDirection();
            }
        }
    }

    private void MoveDown() {
        transform.Translate(Vector3.down * waveData.GetMoveDownAmount());
    }
}