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


    [SerializeField] private WaveData waveData;
    [SerializeField] private IntVariable score;
    [SerializeField] private Text highScoreDisplay;
    private readonly int mainScreenIndex = 1;

    private int highScore;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadFromFile();
        highScoreDisplay.text = highScore.ToString("0000");
    }

    public void GameOver() {
        waveData.Freeze();
        SaveToFile();
    }

    private void Update() {
        if(Input.GetMouseButton(0)) {
            SceneManager.LoadScene(mainScreenIndex);
        }
    }

    public void SaveToFile() {
        
        int currentScore = score.GetValue();
        Debug.Log($"saving to file. current score = {currentScore}, high score = {highScore}");
        if (currentScore > highScore) {
            // save new high score
            SaveData data = new SaveData();
            data.HighScore = currentScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadFromFile() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.HighScore;
        }
    }
}
