using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExecutionPhase))]
public class GamePhaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ExecutionPhase myTarget = (ExecutionPhase)target;

        if (GUILayout.Button("Start Phase"))
        {
            myTarget.StartTimer();
        }

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}
