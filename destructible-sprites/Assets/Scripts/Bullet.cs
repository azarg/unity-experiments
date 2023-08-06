using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bullet : HitSource
{
    [SerializeField] float speed = 1f;
    [SerializeField] float hitDuration = 1f;
    
    private bool hitRegistered = false;

    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var texture = spriteRenderer.sprite.texture;
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        Vector2 coordinateOffset = new(texture.width / 2, texture.height / 2);

        Pixels = new Vector2[texture.width * texture.height];
        for (int x = 0; x < texture.width; x++) {
            for (int y = 0; y < texture.height; y++) {
                var pixel = texture.GetPixel(x, y);
                if (pixel.a != 0) {
                    var pixelCoordinate = new Vector2(x, y);
                    pixelCoordinate -= coordinateOffset;
                    Vector2 localCoordinate = pixelCoordinate / pixelsPerUnit;
                    Pixels[PixelCount] = localCoordinate;
                    PixelCount++;
                }
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y > 7 || transform.position.y < -7 || transform.position.x > 7 || transform.position.x < -7) {
            Destroy(gameObject);
        }
    }

    public override void RegisterHit() {
        if (hitRegistered) return;
        StartCoroutine(DestroyAfterDelay());
        hitRegistered = true;
    }
    
    private IEnumerator DestroyAfterDelay() {
        yield return new WaitForSeconds(hitDuration);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent<Bunker>(out var bunker))
            bunker.Hit(this);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.TryGetComponent<Bunker>(out var bunker))
            bunker.Hit(this);
    }
}
