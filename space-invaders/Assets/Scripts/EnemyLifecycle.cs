using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifecycle : MonoBehaviour {
    [SerializeField] private IntVariable score;
    [SerializeField] private WaveData waveData;
    [SerializeField] private int scoreValue;

    private void OnEnable() {
        waveData.AddEnemy();
    }

    private void OnDestroy() {
        score.Increase(this.scoreValue);
        waveData.IncreaseWaveSpeed();
        waveData.RemoveEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent<Bullet>(out _)) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }


}