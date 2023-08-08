using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] SpriteRenderer splashRenderer;
    [SerializeField] BulletDirection direction;
    [SerializeField] float speed;
    [SerializeField] BoundsVariable bounds;

    public Vector2[] Pixels { get; private set; }
    public int PixelCount { get; private set; }
    private bool hitRegistered = false;
    private Texture2D texture;

    void Start() {
        //Time.timeScale = 0.1f;
        texture = splashRenderer.sprite.texture;
    }
    
    private void Update() {
        if (direction == BulletDirection.DOWN && transform.position.y > bounds.bottom) {
            // move down until reaching the bottom
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        else if (direction == BulletDirection.UP && transform.position.y < bounds.top) {
            // move up until reaching the top
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        } 
        else {
            Destroy(gameObject);
        }
    }

    public void RegisterHit() {
        if (hitRegistered) return;
        
        gameObject.SetActive(false);
        Destroy(gameObject);
        hitRegistered = true;
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
