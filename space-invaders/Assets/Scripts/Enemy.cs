using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private int scoreValue;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent<Bullet>(out _)) {

            Game.data.IncreaseScore(this.scoreValue);
            Wave.data.IncreaseWaveSpeed();
            Wave.data.RemoveEnemy();

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}