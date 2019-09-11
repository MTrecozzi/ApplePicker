using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SFX_II))]
public class SFX_Inspector : Editor
{
  
    public override void OnInspectorGUI(){

        SFX_II sfx = (SFX_II)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Populate Lists")){

            sfx.PopulateLists();
            Debug.Log("Populated SFX_II Lists");
        }

    }

}
