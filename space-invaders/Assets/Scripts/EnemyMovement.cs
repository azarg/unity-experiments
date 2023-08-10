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
        if (GameManager.Instance.gameData.IsEnemyMovementPaused()) return;

        Direction currentDirection = GameManager.Instance.waveData.GetDirection();
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
        float animationSpeed = GameManager.Instance.waveData.GetAnimationSpeed();
        if (animationSpeed != lastAnimationSpeed) {
            animator.SetFloat(SPEED, animationSpeed);
            lastAnimationSpeed = animationSpeed;
        }
    }

    private void MoveInDirection(Direction direction) {
        var moveVector = direction == Direction.RIGHT ? Vector3.right : Vector3.left;
        transform.Translate(GameManager.Instance.waveData.GetWaveSpeed() * Time.deltaTime * moveVector);
    }

    private void ChangeDirectionIfBoundsHit() {
        var direction = GameManager.Instance.waveData.GetDirection();
        if (direction == Direction.RIGHT) {
            if (transform.position.x > GameManager.Instance.boundsData.right) {
                GameManager.Instance.waveData.SetDirection(Direction.LEFT);
            }
        }
        if (direction == Direction.LEFT) {
            if (transform.position.x < GameManager.Instance.boundsData.left) {
                GameManager.Instance.waveData.SetDirection(Direction.RIGHT);
            }
        }
    }

    private void MoveDown() {
        transform.Translate(Vector3.down * GameManager.Instance.boundsData.enemySize * moveDownBy);
    }

    private void HandleEnemyInvasion() {
        if (transform.position.y < GameManager.Instance.boundsData.invastionLine) {
            GameManager.Instance.gameData.GameOver();
        }
    }
}