using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Bunker[] bunkers;

    internal readonly int columns = 11;
    internal readonly int rows = 5;
    internal GameObject[,] enemies;

    private readonly float waveStartDelay = 0.2f;
    private int waveNumber;

    private void Start () {
        enemies = new GameObject[rows, columns];
        GameManager.Instance.waveData.OnNoEnemiesLeft += WaveData_OnNoEnemiesLeft;
        waveNumber = 1;
        StartWaveDelayed();
    }

    private void WaveData_OnNoEnemiesLeft() {
        if (GameManager.Instance.gameData.IsGamePaused()) return;
        waveNumber++;
        GameManager.Instance.waveData.ResetSpeedForWave(waveNumber);
        StartWaveDelayed();
    }

    private void StartWaveDelayed() {
        InvokeRepeating(nameof(StartWave), waveStartDelay, 0);
    }
    private void StartWave () {
        ResetBunkers();
        SetupEnemies();
        InvokeRepeating(nameof(HandleFire), 1, GameManager.Instance.waveData.GetFireRate());
    }

    private void ResetBunkers() {
        for(int i = 0; i< bunkers.Length; i++) {
            if(bunkers[i] != null)
                bunkers[i].ResetSprite();
        }
    }
    
    private void CreateEnemy(int row, int col, int index) {
        float box_size = GameManager.Instance.boundsData.enemySize;
        var enemy = Instantiate(enemyPrefabs[index]);
        enemy.transform.position = new Vector3(GameManager.Instance.boundsData.left + col * box_size + box_size / 2, GameManager.Instance.boundsData.top - row * box_size - box_size / 2, 0);
        enemies[row, col] = enemy;
        GameManager.Instance.waveData.AddEnemy();
    }

    private void SetupEnemies () {
        // row 1
        for (int c = 0; c < columns; c++) {
            CreateEnemy(0, c, 0);
        }
        // rows 2-3
        for (int r = 1; r < 3; r++) {
            for (int c = 0; c < columns; c++) {
                CreateEnemy(r, c, 1);
            }
        }
        // rows 4-5
        for (int r = 3; r < 5; r++) {
            for (int c = 0; c < columns; c++) {
                CreateEnemy(r, c, 2);
            }
        }
    }

    private void HandleFire() {
        if (GameManager.Instance.gameData.IsGamePaused()) return;

        var doFire = Random.Range(0f, 1f) < GameManager.Instance.waveData.GetFireChance();
        if (!doFire) return;

        // randomly select column
        int column = Random.Range(0, columns);

        // find enemy that is at the bottom of the column
        for (int i = rows - 1; i >= 0; i--) {
            if (enemies[i, column] != null) {
                var pos = enemies[i, column].transform.position;
                pos.y -= GameManager.Instance.boundsData.enemySize / 2;
                int bulletIndex = Random.Range(0, 3);
                Instantiate(bulletPrefabs[bulletIndex], pos, Quaternion.identity);
                break;
            }
        }
    }
}