using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveData {
    public int HighScore;
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private readonly int mainScreenIndex = 1;
    private readonly int startScreenIndex = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Game.data.OnGameOver += OnGameOver;
        LoadFromFile();
    }

    private void OnGameOver() {
        SaveToFile();
    }

    private void Update() {
        if(Input.GetMouseButton(0)) {
            if (SceneManager.GetActiveScene().buildIndex == startScreenIndex) {
                SceneManager.LoadScene(mainScreenIndex);
                Game.data.ResetGame();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (SceneManager.GetActiveScene().buildIndex == mainScreenIndex)
                SceneManager.LoadScene(startScreenIndex);
        }
    }

    public void SaveToFile() {
        int currentScore = Game.data.GetScore();
        if (currentScore > Game.data.GetHighScore()) {
            // save new high score
            SaveData data = new SaveData();
            data.HighScore = currentScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            Game.data.SetHighScore(currentScore);
        }
    }

    public void LoadFromFile() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Game.data.SetHighScore(data.HighScore);
        }
    }
}
