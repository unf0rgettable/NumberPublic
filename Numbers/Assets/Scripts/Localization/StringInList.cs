using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Localization;
using TMPro;
using UnityEditor;
#endif

public class StringInList : PropertyAttribute {
    public delegate string[] GetStringList();

    public StringInList(params string [] list) {
        List<string> temp = new List<string>();
        temp.Add("SearchID");
        foreach (var item in list)
        {
            temp.Add(item);
        }

        List = temp.ToArray();
    }

    public StringInList(Type type, string methodName) {
        var method = type.GetMethod (methodName);
        if (method != null) {
            List = method.Invoke (null, null) as string[];
        } else {
            Debug.LogError ("NO SUCH METHOD " + methodName + " FOR " + type);
        }
    }

    public string[] List {
        get;
        private set;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(StringInList))]
public class StringInListDrawer : PropertyDrawer {
    public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) {
        return EditorGUIUtility.singleLineHeight * 2 + 6;
    }
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty( position, label, property );
        var stringInList = attribute as StringInList;
        var list = stringInList.List;
        if (property.propertyType == SerializedPropertyType.String) {
            int index = Mathf.Max (0, Array.IndexOf (list, property.stringValue));
            index = EditorGUI.Popup (position, property.displayName, index, list);
            
            property.stringValue = list [index];
            
            if (index == 0)
            {
                var TextRect = new Rect( position.x, position.y + 20, position.width, 16 );
                
                property.stringValue = EditorGUI.TextField(TextRect ,property.stringValue);
            }

            if (Selection.activeGameObject != null)
            {
                if (!Application.isPlaying)
                {
                    Selection.activeGameObject.GetComponent<TextMeshProUGUI>().text = StaticRes.GetTextByKey(property.stringValue);
                }
            }
        } else if (property.propertyType == SerializedPropertyType.Integer) {
            property.intValue = EditorGUI.Popup (position, property.displayName, property.intValue, list);
        } else {
            base.OnGUI (position, property, label);
        }
        EditorGUI.EndProperty();
    }
}
#endif