using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed;
    [SerializeField] BoundsVariable bounds;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Transform bulletSpawn;

    private void Update() {
        HandleMovement();
        HandleFire();
    }

    private void HandleMovement() {
        var inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            //move right
            if (transform.position.x < bounds.right)
                inputVector += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            //move left
            if (transform.position.x > bounds.left)
                inputVector += Vector3.left;
        }

        transform.Translate(moveSpeed * Time.deltaTime * inputVector);
    }

    private void HandleFire() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        }
    }
}