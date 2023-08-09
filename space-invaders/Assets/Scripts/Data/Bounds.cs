using UnityEditor;

public static class Bounds {

    private readonly static string boundsDataAssetPath = "Assets/Scripts/Data/Bounds.asset";

    public static BoundsData data;

    static Bounds() {
        data = AssetDatabase.LoadAssetAtPath<BoundsData>(boundsDataAssetPath);
    }
}
