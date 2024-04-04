using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(SellerController))]
public class SellerController_Editor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SellerController e = (SellerController)target;


        //GUIStyle GUIS = new GUIStyle();
        GUIStyle GUILabel = new GUIStyle(GUI.skin.GetStyle("label"));
        GUILabel.fontStyle = FontStyle.Bold;
        GUILabel.fontSize = 18;
        
        GUILayout.Label("Test Area",GUILabel);
        
        if (GUILayout.Button("Hide Teleporter")) e.SetToHide();
        if (GUILayout.Button("Set To Animation")) e.SetToAnimation();
        if (GUILayout.Button("Set To Build")) e.SetToBuild();
        if (GUILayout.Button("Set To Obstructed")) e.SetToObstructed();
        

        if (Application.isPlaying) { 
        GUILayout.Label("Methods Automap", GUILabel);

        GUIStyle GUIButton = new GUIStyle(GUI.skin.GetStyle("button"));
        GUIButton.fontSize = 10;
        
        GUIButton.normal.textColor = Color.white;
        GUIButton.alignment = TextAnchor.MiddleLeft;

        MethodInfo[] methods = e.GetType().GetMethods();
        foreach (MethodInfo method in methods) 
        {
            if (method.IsPublic && method.DeclaringType.Name==e.GetType().Name) 
            {
                if (GUILayout.Button(method.Name,GUIButton)) { e.SendMessage(method.Name); };
            }
        }

        }

    }

}
