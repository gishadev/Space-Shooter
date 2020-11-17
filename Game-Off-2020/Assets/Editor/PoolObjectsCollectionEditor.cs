using SpaceGame.Optimisation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoolObjectsCollection))]
public class PoolObjectsCollectionEditor : Editor
{
    PoolObjectsCollection _collection;
    SerializedProperty _poolObjectsProp;

    private void OnEnable()
    {
        _collection = (PoolObjectsCollection)target;

        _poolObjectsProp = serializedObject.FindProperty("poolObjects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_poolObjectsProp, true);

        if (GUILayout.Button("Reset Collection"))
            _collection.Reset();

        serializedObject.ApplyModifiedProperties();
    }
}
