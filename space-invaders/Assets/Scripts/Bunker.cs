using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Texture2D texture;
    private int width;
    private int height;
    private float pixelsPerUnit;
    private Vector2 pixelCoordinateOffset;

    private void Start() {
        // Create a copy of the bunker sprite since we dont want to modify the original sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CopySprite(spriteRenderer);
        texture = spriteRenderer.sprite.texture;
        width = texture.width;
        height = texture.height;
        pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        pixelCoordinateOffset = new Vector2(width / 2, height / 2);
    }
    private Sprite CopySprite(SpriteRenderer spriteRenderer) {
        var _texture = spriteRenderer.sprite.texture;
        var copy = new Texture2D(_texture.width, _texture.height, _texture.format, false);
        copy.SetPixels(_texture.GetPixels());
        copy.Apply();
        var sprite = Sprite.Create(copy, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);
        sprite.texture.filterMode = spriteRenderer.sprite.texture.filterMode;
        return sprite;
    }
    public void Hit(Bullet bullet) {
        bool hit = false;
        for (int i = 0; i < bullet.PixelCount; i++) {
            Vector2 r = GetTargetPixelCoordinate(bullet.Pixels[i], bullet);
            int target_x = (int)r.x;
            int target_y = (int)r.y;
            if (target_x >= 0 && target_x < width) {
                if (target_y >= 0 && target_y < height) {
                    var pixel = texture.GetPixel(target_x, target_y);
                    if (pixel.a != 0) {
                        hit = true;
                        pixel.a = 0f;
                        texture.SetPixel(target_x, target_y, pixel);
                    }
                }
            }
        }
        if (hit) {
            bullet.RegisterHit();
            texture.Apply();
        }
    }
    private Vector2 GetTargetPixelCoordinate(Vector2 sourceLocalPosition, Bullet bullet) {
        var woldPosition = bullet.transform.TransformPoint(sourceLocalPosition);
        var targetLocalPosition = (Vector2)transform.InverseTransformPoint(woldPosition);
        var targetPixelCooridinate = targetLocalPosition * pixelsPerUnit;
        targetPixelCooridinate += pixelCoordinateOffset;
        return targetPixelCooridinate;
    }
}