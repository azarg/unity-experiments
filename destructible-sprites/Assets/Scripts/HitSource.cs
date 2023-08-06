using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSource : MonoBehaviour {
    public Vector2[] Pixels { get; protected set; }
    public int PixelCount { get; protected set; }
    public virtual void RegisterHit() { }
}
