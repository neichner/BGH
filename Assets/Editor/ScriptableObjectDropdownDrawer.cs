using NE.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NE.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableObjectDropdownAttribute))]
    public class ScriptableObjectDropdownDrawer : PropertyDrawer
    {
        private static List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();
        private static ScriptableObject selectedScriptableObject;
        private static readonly int controlHint = typeof(ScriptableObjectDropdownAttribute).GetHashCode();
        private static GUIContent popupContent = new GUIContent();
        private static int selectedControlID;
        private static readonly GenericMenu.MenuFunction2 onSelectedScriptableObject = OnSelectedScriptableObject;
        private static bool isChanged;

        static ScriptableObjectDropdownDrawer()
        {
            EditorApplication.projectChanged += ClearCache;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (scriptableObjects.Count == 0)
            {
                GetScriptableObjects(property);
            }

            Draw(position, label, property, attribute as ScriptableObjectDropdownAttribute);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorStyles.popup.CalcHeight(GUIContent.none, 0);
        }

        /// <summary>
        /// How you can get type of field which it uses PropertyAttribute
        /// </summary>
        private static Type GetPropertyType(SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = parentType.GetField(property.propertyPath);
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }
            return null;
        }

        private static bool ValidateProperty(SerializedProperty property)
        {
            Type propertyType = GetPropertyType(property);
            if (propertyType == null)
            {
                return false;
            }
            if (!propertyType.IsSubclassOf(typeof(ScriptableObject)) && propertyType != typeof(ScriptableObject))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// When new ScriptableObject added to the project
        /// </summary>
        private static void ClearCache()
        {
            scriptableObjects.Clear();
        }

        /// <summary>
        /// Gets ScriptableObjects just when it is a first time or new ScriptableObject added to the project
        /// </summary>
        private static ScriptableObject[] GetScriptableObjects(SerializedProperty property)
        {
            Type propertyType = GetPropertyType(property);
            string[] guids = AssetDatabase.FindAssets(String.Format("t:{0}", propertyType));
            for (int i = 0; i < guids.Length; i++)
            {
                scriptableObjects.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), propertyType) as ScriptableObject);
            }

            return scriptableObjects.ToArray();
        }

        private void Draw(Rect position, GUIContent label,
            SerializedProperty property, ScriptableObjectDropdownAttribute attribute)
        {
            if (label != null && label != GUIContent.none)
                position = EditorGUI.PrefixLabel(position, label);

            if (ValidateProperty(property))
            {
                if (scriptableObjects.Count != 0)
                {
                    UpdateScriptableObjectSelectionControl(position, label, property, attribute);
                }
                else
                {
                    EditorGUI.LabelField(position, "There is no this type asset in the project");
                }
            }
            else
            {
                EditorGUI.LabelField(position, "Use it with non-array ScriptableObject or derived class of ScriptableObject");
            }
        }

        private static void UpdateScriptableObjectSelectionControl(Rect position, GUIContent label,
            SerializedProperty property, ScriptableObjectDropdownAttribute attribute)
        {
            ScriptableObject output = DrawScriptableObjectSelectionControl(position, label, property.objectReferenceValue as ScriptableObject, property, attribute);

            if (isChanged)
            {
                isChanged = false;
                property.objectReferenceValue = output;
            }
        }

        private static ScriptableObject DrawScriptableObjectSelectionControl(Rect position, GUIContent label,
            ScriptableObject scriptableObject, SerializedProperty property, ScriptableObjectDropdownAttribute attribute)
        {
            bool triggerDropDown = false;
            int controlID = GUIUtility.GetControlID(controlHint, FocusType.Keyboard, position);

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.ExecuteCommand:
                    if (Event.current.commandName == "ScriptableObjectReferenceUpdated")
                    {
                        if (selectedControlID == controlID)
                        {
                            if (scriptableObject != selectedScriptableObject)
                            {
                                scriptableObject = selectedScriptableObject;
                                isChanged = true;
                            }

                            selectedControlID = 0;
                            selectedScriptableObject = null;
                        }
                    }
                    break;

                case EventType.MouseDown:
                    if (GUI.enabled && position.Contains(Event.current.mousePosition))
                    {
                        GUIUtility.keyboardControl = controlID;
                        triggerDropDown = true;
                        Event.current.Use();
                    }
                    break;

                case EventType.KeyDown:
                    if (GUI.enabled && GUIUtility.keyboardControl == controlID)
                    {
                        if (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.Space)
                        {
                            triggerDropDown = true;
                            Event.current.Use();
                        }
                    }
                    break;

                case EventType.Repaint:
                    if (scriptableObject == null)
                    {
                        popupContent.text = "Nothing";
                    }
                    else
                    {
                        popupContent.text = scriptableObject.name;
                    }
                    EditorStyles.popup.Draw(position, popupContent, controlID);
                    break;
            }

            if (scriptableObjects.Count != 0 && triggerDropDown)
            {
                selectedControlID = controlID;
                selectedScriptableObject = scriptableObject;

                DisplayDropDown(position, scriptableObject);
            }

            return scriptableObject;
        }

        private static void DisplayDropDown(Rect position, ScriptableObject selectedScriptableObject)
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("Nothing"), selectedScriptableObject == null, onSelectedScriptableObject, null);
            menu.AddSeparator("");

            for (int i = 0; i < scriptableObjects.Count; ++i)
            {
                var scriptableObject = scriptableObjects[i];

                string menuLabel = MakeDropDownGroup(scriptableObject);
                if (string.IsNullOrEmpty(menuLabel))
                    continue;

                var content = new GUIContent(menuLabel);
                menu.AddItem(content, scriptableObject == selectedScriptableObject, onSelectedScriptableObject, scriptableObject);
            }

            menu.DropDown(position);
        }

        private static void OnSelectedScriptableObject(object userData)
        {
            selectedScriptableObject = userData as ScriptableObject;
            var scriptableObjectReferenceUpdatedEvent = EditorGUIUtility.CommandEvent("ScriptableObjectReferenceUpdated");
            EditorWindow.focusedWindow.SendEvent(scriptableObjectReferenceUpdatedEvent);
        }

        private static string FindScriptableObjectFolderPath(ScriptableObject scriptableObject)
        {
            string path = AssetDatabase.GetAssetPath(scriptableObject);
            path = path.Replace("Assets/", "");
            path = path.Replace(".asset", "");

            return path;
        }

        private static string MakeDropDownGroup(ScriptableObject scriptableObject)
        {
            string path = FindScriptableObjectFolderPath(scriptableObject);
            path = path.Replace("/", " > ");
            return path;
        }
    }
}
