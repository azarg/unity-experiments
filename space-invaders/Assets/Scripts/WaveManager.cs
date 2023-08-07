using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private WaveData waveData;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] BoundsVariable bounds;

    private void Start () {
        waveData.OnNoEnemiesLeft += WaveData_OnNoEnemiesLeft;
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
        int columns = 11;
        GameObject prefab;
        float box_size = bounds.enemySize;

        // row 1
        prefab = enemyPrefabs[0];
        for (int c = 0; c < columns; c++) {
            var enemy = Instantiate(prefab);
            enemy.transform.position = new Vector3(bounds.left + c * box_size + box_size / 2, bounds.top - box_size / 2, 0);
        }


        // rows 2-3
        float row_y = bounds.top - box_size;
        prefab = enemyPrefabs[1];
        for (int i = 0; i < 2; i++) {
            for (int c = 0; c < columns; c++) {
                var enemy = Instantiate(prefab);
                enemy.transform.position = new Vector3(bounds.left + c * box_size + box_size / 2, row_y - i * box_size - box_size / 2, 0);
            }
        }

        // rows 4-5
        row_y -= 2 * box_size;
        prefab = enemyPrefabs[2];
        for (int i = 0; i < 2; i++) {
            for (int c = 0; c < columns; c++) {
                var enemy = Instantiate(prefab);
                enemy.transform.position = new Vector3(bounds.left + c * box_size + box_size / 2, row_y - i * box_size - box_size / 2, 0);
            }
        }

    }
}