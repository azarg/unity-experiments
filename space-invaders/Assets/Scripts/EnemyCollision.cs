using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

    [SerializeField] WaveData waveData;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent<Bullet>(out _)) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            waveData.IncreaseWaveSpeed();
        }
    }
}