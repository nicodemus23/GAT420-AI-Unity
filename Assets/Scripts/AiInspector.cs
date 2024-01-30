using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AiInspector : EditorWindow
{
    [MenuItem("Ai/Inspector")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AiInspector));
    }

    private void OnGUI()
    {
        //GUILayout.BeginHorizontal(); 
        GUILayout.Label("Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("View Agent"))
        {
            Camera camera = Camera.main;
            GameObject go = Selection.activeGameObject;
            // TODO: Check if the selected object is an AiNavAgent
            if (go.TryGetComponent<AiNavAgent>(out AiNavAgent agent))
            {
                camera.transform.parent = agent.transform; // Set the camera's parent to the agent
                camera.transform.localPosition = Vector3.back * 5 + Vector3.up * 2; // Set the camera's position
                camera.transform.localRotation = Quaternion.identity; // Reset the camera's rotation
            }
        }
    }
}
