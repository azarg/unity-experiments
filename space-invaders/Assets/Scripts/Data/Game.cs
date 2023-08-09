using UnityEditor;

public static class Game {

    private readonly static string gameDataAssetPath = "Assets/Scripts/Data/GameData.asset";

    public static GameData data;

    static Game() {
        data = AssetDatabase.LoadAssetAtPath<GameData>(gameDataAssetPath);
    }
}