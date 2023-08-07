using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifecycle : MonoBehaviour {

    [SerializeField] WaveData waveData;
    [SerializeField] BoundsVariable bounds;

    private void OnEnable() {
        waveData.AddEnemy();
    }

    private void OnDestroy() {
        waveData.RemoveEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent<Bullet>(out _)) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            waveData.IncreaseWaveSpeed();
        }
    }


}