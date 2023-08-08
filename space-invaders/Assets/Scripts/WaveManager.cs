using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private WaveData waveData;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] BoundsVariable bounds;
    internal readonly int columns = 11;
    internal readonly int rows = 5;
    internal GameObject[,] enemies;

    private void Start () {
        enemies = new GameObject[rows, columns];
        waveData.OnNoEnemiesLeft += WaveData_OnNoEnemiesLeft;
        InvokeRepeating(nameof(StartWave), 1, 0);
    }

    private void WaveData_OnNoEnemiesLeft() {
        Debug.Log("wave is over");
    }

    private void StartWave () {
        SetupEnemies();
        InvokeRepeating(nameof(HandleFire), 1, waveData.GetFireRate());
    }

    private void SetupEnemies () {
        float box_size = bounds.enemySize;

        // row 1
        for (int c = 0; c < columns; c++) {
            var enemy = Instantiate(enemyPrefabs[0]);
            enemy.transform.position = new Vector3(bounds.left + c * box_size + box_size / 2, bounds.top - box_size / 2, 0);
            enemies[1, c] = enemy;
        }

        // rows 2-3
        float row_y = bounds.top - box_size;
        for (int i = 0; i < 2; i++) {
            for (int c = 0; c < columns; c++) {
                var enemy = Instantiate(enemyPrefabs[1]);
                enemy.transform.position = new Vector3(bounds.left + c * box_size + box_size / 2, row_y - i * box_size - box_size / 2, 0);
                enemies[i + 1, c] = enemy;
            }
        }

        // rows 4-5
        row_y -= 2 * box_size;
        for (int i = 0; i < 2; i++) {
            for (int c = 0; c < columns; c++) {
                var enemy = Instantiate(enemyPrefabs[2]);
                enemy.transform.position = new Vector3(bounds.left + c * box_size + box_size / 2, row_y - i * box_size - box_size / 2, 0);
                enemies[i + 3, c] = enemy;
            }
        }
    }

    private void HandleFire() {
        var doFire = Random.Range(0f, 1f) < waveData.GetFireChance();
        if (!doFire) return;

        // randomly select column
        int column = Random.Range(0, columns);

        // find enemy that is at the bottom of the column
        for (int i = rows - 1; i >= 0; i--) {
            if (enemies[i, column] != null) {
                var pos = enemies[i, column].transform.position;
                pos.y -= bounds.enemySize / 2;
                Instantiate(bulletPrefab, pos, Quaternion.identity);
                break;
            }
        }
    }
}