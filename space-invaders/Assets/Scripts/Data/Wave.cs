using UnityEditor;

public static class Wave {

    private readonly static string waveDataAssetPath = "Assets/Scripts/Data/WaveData.asset";

    public static WaveData data;

    static Wave() {
        data = AssetDatabase.LoadAssetAtPath<WaveData>(waveDataAssetPath);
    }
}