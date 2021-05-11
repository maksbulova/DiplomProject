using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Node script = (Node)target;
        /*
        if (GUILayout.Button("Manual init"))
        {
            script.ManualInit();
        }
        */
    }
}
