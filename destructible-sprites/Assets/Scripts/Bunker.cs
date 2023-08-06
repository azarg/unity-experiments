using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Profiling;

public class Bunker : MonoBehaviour {

    private Texture2D texture;
    private int width;
    private int height;
    private float pixelsPerUnit;
    private Vector2 pixelCoordinateOffset;

    void Start() {
        var spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void Hit(HitSource bullet) {
        bool hit = false;
        for (int i = 0; i < bullet.PixelCount; i++) {
            //Profiler.BeginSample("get coordinates");
            Vector2 r = GetTargetPixelCoordinate(bullet.Pixels[i], bullet);
            //Profiler.EndSample();
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
    private Vector2 GetTargetPixelCoordinate(Vector2 sourceLocalPosition, HitSource bullet) {
        var woldPosition = bullet.transform.TransformPoint(sourceLocalPosition);
        var targetLocalPosition = (Vector2)transform.InverseTransformPoint(woldPosition);
        var targetPixelCooridinate = targetLocalPosition * pixelsPerUnit;
        targetPixelCooridinate += pixelCoordinateOffset;
        return targetPixelCooridinate;
    }


    //public void Hit_v2(Vector3 sourceWorldPosition, Texture2D sourceTexture, Bullet bullet) {
    //    bool hit = false;
    //    for (int x = 0; x < sourceTexture.width; x++) {
    //        for (int y = 0; y < sourceTexture.height; y++) {
    //            Vector2 r = GetTargetPixelCoordinate(new Vector2(x, y), sourceTexture, bullet);
    //            int target_x = (int)r.x;
    //            int target_y = (int)r.y;
    //            if (target_x >= 0 && target_x < texture.width) {
    //                if (target_y >= 0 && target_y < texture.height) {
    //                    var source_pixel = sourceTexture.GetPixel(x, y);
    //                    if (source_pixel.a != 0) {
    //                        var pixel = texture.GetPixel(target_x, target_y);
    //                        if (pixel.a != 0) {
    //                            hit = true;
    //                            pixel.a = 0f;
    //                            texture.SetPixel(target_x, target_y, pixel);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    if (hit) {
    //        bullet.RegisterHit();
    //        texture.Apply();
    //    }
    //}

    //private Vector2 GetTargetPixelCoordinate(Vector2 sourcePixelCoordinate_self, Texture2D sourceTexture, Bullet bullet) {
    //    sourcePixelCoordinate_self -= new Vector2(sourceTexture.width / 2, sourceTexture.height / 2);
    //    Vector2 sourceLocalPosition_self = sourcePixelCoordinate_self / pixelsPerUnit;
    //    var woldPosition = bullet.transform.TransformPoint(sourceLocalPosition_self);
    //    var sourceLocalPosition_target = (Vector2)transform.InverseTransformPoint(woldPosition);
    //    var sourcePixelPosition_target = (Vector2)sourceLocalPosition_target * pixelsPerUnit;
    //    sourcePixelPosition_target += new Vector2(texture.width / 2, texture.height / 2);
    //    return sourcePixelPosition_target;
    //}
    //public void Hit(Vector3 sourceWorldPosition, Texture2D sourceTexture, Bullet bullet) {
    //    Calculate the source(bullet) position relative to target(bunker) position in pixels
    //    var sourceLocalPosition = transform.InverseTransformPoint(sourceWorldPosition);
    //    Vector2 sourcePixelPosition = sourceLocalPosition * pixelsPerUnit;
    //    sourcePixelPosition -= new Vector2(sourceTexture.width / 2, sourceTexture.height / 2);
    //    sourcePixelPosition += new Vector2(texture.width / 2, texture.height / 2);
    //    bool hit = false;
    //    for (int x = 0; x < sourceTexture.width; x++) {
    //        int target_x = (int)(sourcePixelPosition.x + x);
    //        if (target_x < 0 || target_x > texture.width) continue;
    //        for (int y = 0; y < sourceTexture.height; y++) {
    //            int target_y = (int)(sourcePixelPosition.y + y);
    //            if (target_y < 0 || target_y > texture.height) continue;

    //            var sourcePixel = sourceTexture.GetPixel(x, y);
    //            if (sourcePixel.a != 0) {
    //                var targetPixel = texture.GetPixel(target_x, target_y);
    //                if (targetPixel.a != 0) {
    //                    hit = true;
    //                    targetPixel.a = 0f;
    //                    texture.SetPixel(target_x, target_y, targetPixel);
    //                }
    //            }
    //        }
    //    }
    //    if (hit) {
    //        bullet.RegisterHit();
    //        texture.Apply();
    //    }
    //}
}
