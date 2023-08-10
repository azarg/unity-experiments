using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float fireDelay = 1f;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Transform bulletSpawn;

    [SerializeField] GameObject defaultVisual;
    [SerializeField] GameObject destroyVisual;

    private bool canFire = true;
    private float fireTime;
    private float destroyAnimationDuration = 1f;

    private void Update() {
        if (Game.data.IsGamePaused()) return;
        HandleMovement();
        HandleFire();
    }

    private void HandleMovement() {
        var inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            //move right
            if (transform.position.x < Bounds.data.right)
                inputVector += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            //move left
            if (transform.position.x > Bounds.data.left)
                inputVector += Vector3.left;
        }

        if (inputVector != Vector3.zero) {
            transform.Translate(Game.data.GetPlayerSpeed() * Time.deltaTime * inputVector);
        }
    }

    private void HandleFire() {
        // enable fire after delay
        if (fireTime + fireDelay < Time.time) {
            canFire = true;
        }
        if (canFire) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                canFire = false;
                fireTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent<Bullet>(out _)){
            Game.data.DecreasePlayerLives();
            Game.data.PauseGame();

            StartCoroutine(PlayKillAnimation());

            Destroy(collision.gameObject);
        }
    }

    IEnumerator PlayKillAnimation() {
        
        defaultVisual.SetActive(false);
        destroyVisual.SetActive(true);
        yield return new WaitForSeconds(destroyAnimationDuration);
        Game.data.UnpauseGame();

        defaultVisual.SetActive(true);
        destroyVisual.SetActive(false);
    }
}