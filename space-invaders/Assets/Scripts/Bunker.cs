using UnityEngine;

public class Bunker : MonoBehaviour {
    [SerializeField] SpriteRenderer splashRenderer;
    [SerializeField] int minBulletPenetration = -5;
    [SerializeField] int maxBulletPenetration = 1;

    private SpriteRenderer spriteRenderer;
    private Texture2D bunkerTexture;
    private Texture2D splashTexture;
    private Sprite originalSprite;
    private int width;
    private int height;
    private float pixelsPerUnit;
    private Vector2 pixelCoordinateOffset;
    
    private void Start() {
        splashTexture = splashRenderer.sprite.texture;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        // Create a copy of the bunker sprite since we dont want to modify the original sprite
        ResetSprite();
        width = bunkerTexture.width;
        height = bunkerTexture.height;
        pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        pixelCoordinateOffset = new Vector2(width / 2, height / 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Bullet>(out var bullet)) {
            var result = Hit(bullet.transform.position, bullet.GetDirection());
            if (result) {
                bullet.gameObject.SetActive(false);
                Destroy(bullet.gameObject);
            }
        }
    }

    public void ResetSprite() {
        var _texture = originalSprite.texture;
        var copy = new Texture2D(_texture.width, _texture.height, _texture.format, false);
        copy.SetPixels(_texture.GetPixels());
        copy.Apply();
        var newSprite = Sprite.Create(copy, originalSprite.rect, new Vector2(0.5f, 0.5f), originalSprite.pixelsPerUnit);
        newSprite.texture.filterMode = originalSprite.texture.filterMode;
        spriteRenderer.sprite = newSprite;
        bunkerTexture = spriteRenderer.sprite.texture;
    }

    private void DrawSplash(int center_x, int center_y, BulletDirection dir, Texture2D splash) {
        int offset = Random.Range(minBulletPenetration, maxBulletPenetration);
        int dy = dir == BulletDirection.UP ? 1 : -1;
        if (offset > 0) {
            for (int i = 0; i < offset; i++) {
                var pixel = bunkerTexture.GetPixel(center_x, center_y + i * dy);
                pixel.a = 0f;
                bunkerTexture.SetPixel(center_x, center_y + i * dy, pixel);
            }
        }
        for (int x = 0; x < splash.width; x++) {
            for (int y = 0;  y < splash.height; y++) {
                int target_x = center_x - dy * (splash.width / 2 - x);
                int target_y = center_y + dy * (y + offset);
                if (target_x >= 0 && target_x < width && target_y >= 0 && target_y < height) {
                    var sp = splash.GetPixel(x, y);
                    if (sp.a != 0) {
                        var pixel = bunkerTexture.GetPixel(target_x, target_y);
                        if (pixel.a != 0) {
                            pixel.a = 0f;
                            bunkerTexture.SetPixel(target_x, target_y, pixel);
                        }
                    }
                }
            }
        }
    }

    public bool Hit(Vector3 hitPosition, BulletDirection dir) {
        bool hit = false;
        var lp = (Vector2)transform.InverseTransformPoint(hitPosition);
        var pp = lp * pixelsPerUnit + pixelCoordinateOffset;
        var x = (int) pp.x;
        if (x >= 0 && x < width) {
            int start_y, end_y;
            if (dir == BulletDirection.UP) {
                start_y = 0;
                end_y = height - 1;
            } else {
                start_y = height - 1;
                end_y = 0;
            }
            int y = start_y;
            int dy = dir == BulletDirection.UP ? 1 : -1;
            while (y != end_y) {
                var p = bunkerTexture.GetPixel(x, y);
                if (p.a != 0) {
                    hit = true;
                    DrawSplash(x, y, dir, splashTexture);
                    break;
                }
                y += dy;
            }
        }
        if (hit) {
            bunkerTexture.Apply();
        }
        return hit;
    }
}