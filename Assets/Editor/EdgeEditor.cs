﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Edge))]
public class EdgeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Edge script = (Edge)target;
        /*
        if (GUILayout.Button("Manual init"))
        {
            script.ManualInit();
        }
        */
        
    }

}
