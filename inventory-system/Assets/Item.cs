using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public bool stackable = true;
    public GameObject prefab;
}
