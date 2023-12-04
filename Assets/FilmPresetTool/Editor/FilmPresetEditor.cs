using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[CustomEditor(typeof(FilmPreset))]
public class FilmPresetEditor : Editor
{
    private SerializedProperty _None;
    private SerializedProperty _Custom;
    private SerializedProperty volume;
    private SerializedProperty Current;

    private void OnEnable()
    {
        
        _Custom = serializedObject.FindProperty("Custom");
        volume = serializedObject.FindProperty("volume");
        Current = serializedObject.FindProperty("Current");
    }
    public override void OnInspectorGUI()
    {
        FilmPreset data = (FilmPreset)target;
        
        EditorGUILayout.LabelField("Film Preset Tool".ToUpper(), EditorStyles.boldLabel);
        EditorGUILayout.Space(10);
        base.OnInspectorGUI();
        EditorGUILayout.Space(25);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
        {
            data.SettoNew();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(3);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            data.SaveCustom();
        }
        EditorGUILayout.EndHorizontal();
    }
  
   
}
