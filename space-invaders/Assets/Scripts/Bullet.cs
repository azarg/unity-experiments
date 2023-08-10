using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum BulletDirection { UP, DOWN }

public class Bullet : MonoBehaviour {
    [SerializeField] BulletDirection direction;
    [SerializeField] float speed;

    public BulletDirection GetDirection() => direction;
    
    private void Update() {
        if (Game.data.IsGameOver()) return;

        if (direction == BulletDirection.DOWN && transform.position.y > Bounds.data.bottom) {
            // move down until reaching the bottom
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        else if (direction == BulletDirection.UP && transform.position.y < Bounds.data.top) {
            // move up until reaching the top
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        } 
        else {
            Destroy(gameObject);
        }
    }
}
