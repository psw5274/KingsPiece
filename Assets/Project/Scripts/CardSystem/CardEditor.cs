using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardEditor : MonoBehaviour
{ }

/*
[CustomEditor(typeof(BasicCardData))]
public class CardEditor : Editor {

    public override void OnInspectorGUI()
    {
        BasicCardData cardData = (BasicCardData)target;
        EditorGUILayout.TextField("Layout number", cardData.cardName);

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
*/