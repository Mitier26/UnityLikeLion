using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(RedBlackTree))]
public class RBNodeEditor : Editor
{
    public int newKey;

    SerializedProperty rbNodeObjectProp;
    SerializedProperty nilNodeObjectProp;
    void OnEnable()
    {
        rbNodeObjectProp = serializedObject.FindProperty("RBNodePrefab");
        nilNodeObjectProp = serializedObject.FindProperty("nilNode");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        RedBlackTree redBlackTree = (RedBlackTree)target;
        EditorGUILayout.PropertyField(rbNodeObjectProp);
        EditorGUILayout.PropertyField(nilNodeObjectProp);
        
        newKey = EditorGUILayout.IntField("newKey", newKey);
        if (GUILayout.Button("Add Node"))
        {
            redBlackTree.Insert(newKey);   
        }

        serializedObject.ApplyModifiedProperties();
    }
}
