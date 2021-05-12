﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Graph))]

public class GraphEditor : Editor
{
    Vector2 scrollPos;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Graph script = (Graph)target;

        EditorGUILayout.LabelField("Nodes amount", script.nodeList.Count.ToString());

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("from", "to  by");

        EditorGUILayout.Space();

        foreach (KeyValuePair<Node, Dictionary<Node, Edge>> node in script.nodeList)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(node.Key.name);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            EditorGUILayout.BeginVertical();

            foreach (KeyValuePair<Node, Edge> subnode in node.Value)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(subnode.Key.name);
                EditorGUILayout.LabelField(subnode.Value.name);
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndHorizontal();
            
        }
        
        EditorGUILayout.EndVertical();
    }

}
