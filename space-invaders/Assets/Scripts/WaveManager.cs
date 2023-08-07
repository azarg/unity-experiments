using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private WaveData waveData;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] BoundsVariable bounds;

    private void Start () {
        waveData.OnNoEnemiesLeft += WaveData_OnNoEnemiesLeft;
        transform.position = new Vector2(bounds.left, bounds.top);
        StartCoroutine(DelayedWaveStart());
    }

    private void WaveData_OnNoEnemiesLeft() {
        Debug.Log("wave is over");
    }

    IEnumerator DelayedWaveStart () {
        yield return new WaitForSeconds(1);
        StartWave();
    }

    private void StartWave () {
        // number of columns and rows of enemies
        GameObject prefab;
        BoxCollider2D collider = null;
        int columns = 11;

        // row 1
        prefab = enemyPrefabs[0];
        collider = prefab.GetComponent<BoxCollider2D>();
        for (int c = 0; c < columns; c++) {
            var enemy = Instantiate(prefab);
            enemy.transform.position = new Vector3(bounds.left + c * collider.size.x + collider.size.x / 2, bounds.top - collider.size.y / 2, 0);
        }


        // rows 2-3
        float row_y = bounds.top - collider.size.y;
        prefab = enemyPrefabs[1];
        collider = prefab.GetComponent<BoxCollider2D>();
        for (int i = 0; i < 2; i++) {
            for (int c = 0; c < columns; c++) {
                var enemy = Instantiate(prefab);
                enemy.transform.position = new Vector3(bounds.left + c * collider.size.x + collider.size.x / 2, row_y - i * collider.size.y - collider.size.y / 2, 0);
            }
        }

        // rows 4-6
        row_y -= 2 * collider.size.y;
        prefab = enemyPrefabs[2];
        collider = prefab.GetComponent<BoxCollider2D>();
        for (int i = 0; i < 3; i++) {
            for (int c = 0; c < columns; c++) {
                var enemy = Instantiate(prefab);
                enemy.transform.position = new Vector3(bounds.left + c * collider.size.x + collider.size.x / 2, row_y - i * collider.size.y - collider.size.y / 2, 0);
            }
        }

    }
}