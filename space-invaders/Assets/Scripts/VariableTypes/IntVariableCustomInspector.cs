using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IntVariable))]
public class IntVariableCustomInspector : Editor {
    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        var variable = (IntVariable)target;
        if(GUILayout.Button("Trigger change")) {
            variable.SetValue(variable.GetValue());
        }
    }
}