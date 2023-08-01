using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            stats.ChangeHealth(-1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            stats.ChangeHealth(1);
        }

    }
}
