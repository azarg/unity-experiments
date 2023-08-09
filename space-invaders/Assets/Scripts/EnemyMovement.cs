using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] Animator animator;
    private readonly string SPEED = "speed";
    private readonly float moveDownBy = 0.3f;// as a ratio of enemy size

    // need to remember the last direction, because direction may be changed
    // by other enemies. and if it changes, then I need to move down and
    // remember the new direction.
    private Direction lastDirection;

    // remeber the last animation speed so that we dont have to change it every frame
    private float lastAnimationSpeed;

    private void Update() {
        if (Game.data.IsGameOver()) return;

        Direction currentDirection = Wave.data.GetDirection();
        HandleAnimation();
        if (currentDirection != lastDirection) {
            // direction has changed (meaning one of the enemies has hit the bounds)
            // move down, check if enemies have won (invaded)
            // and remember the current direction
            MoveDown();
            HandleEnemyInvasion();
            lastDirection = currentDirection;
        }
        else {
            // direction hasn't changed from last frame, continue moving
            MoveInDirection(currentDirection);
        }
    }

    private void LateUpdate() {
        // change direction if bounds have been hit 
        ChangeDirectionIfBoundsHit();
    }

    private void HandleAnimation() {
        float animationSpeed = Wave.data.GetAnimationSpeed();
        if (animationSpeed != lastAnimationSpeed) {
            animator.SetFloat(SPEED, animationSpeed);
            lastAnimationSpeed = animationSpeed;
        }
    }

    private void MoveInDirection(Direction direction) {
        var moveVector = direction == Direction.RIGHT ? Vector3.right : Vector3.left;
        transform.Translate(Wave.data.GetWaveSpeed() * Time.deltaTime * moveVector);
    }

    private void ChangeDirectionIfBoundsHit() {
        var direction = Wave.data.GetDirection();
        if (direction == Direction.RIGHT) {
            if (transform.position.x > Bounds.data.right) {
                Wave.data.SetDirection(Direction.LEFT);
            }
        }
        if (direction == Direction.LEFT) {
            if (transform.position.x < Bounds.data.left) {
                Wave.data.SetDirection(Direction.RIGHT);
            }
        }
    }

    private void MoveDown() {
        transform.Translate(Vector3.down * Bounds.data.enemySize * moveDownBy);
    }

    private void HandleEnemyInvasion() {
        if (transform.position.y < Bounds.data.invastionLine) {
            Game.data.GameOver();
        }
    }
}