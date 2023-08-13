using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private Item[] itemsToPick;
    
    public void PickupItem(int id) {
        var item = Instantiate(itemsToPick[id].prefab);
        item.transform.position = new Vector3(Random.Range(-5f,5f), Random.Range(-2.5f, 2.5f),0);
    }
}
