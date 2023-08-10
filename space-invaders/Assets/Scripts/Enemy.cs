using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private int scoreValue;
    [SerializeField] private GameObject defaultVisual;
    [SerializeField] private GameObject destroyedVisual;

    private float destroyAnimationDuration = 0.1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent<Bullet>(out _)) {

            Game.data.IncreaseScore(this.scoreValue);
            Wave.data.IncreaseWaveSpeed();
            Wave.data.RemoveEnemy();

            Game.data.PauseEnemyMovement();
            StartCoroutine(PlayKillAnimation());

            Destroy(collision.gameObject);
        }
    }

    IEnumerator PlayKillAnimation() {
        defaultVisual.SetActive(false);
        destroyedVisual.SetActive(true);
        yield return new WaitForSeconds(destroyAnimationDuration);
        Game.data.UnpauseEnemyMovement();
        Destroy(gameObject);
    }
}