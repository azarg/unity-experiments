using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] SpriteRenderer splashRenderer;
    [SerializeField] BulletDirection direction;
    [SerializeField] float speed;

    private Texture2D texture;

    void Start() {
        texture = splashRenderer.sprite.texture;
    }
    
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent<Bunker>(out var bunker)) {
            var result = bunker.Hit(this.transform.position, direction, texture);
            if (result) {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
