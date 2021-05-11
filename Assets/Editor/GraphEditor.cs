using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Graph))]

public class GraphEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Graph script = (Graph)target;

        EditorGUILayout.LabelField("Nodes amount", script.nodeList.Count.ToString());
        /*
        foreach (KeyValuePair<Node, Dictionary<Node, Edge>> node in script.nodeList)
        {
            EditorGUILayout.LabelField()
        }
        */

    }

}
