using MoreMountains.NiceVibrations;
using UnityEditor;
using UnityEngine;

namespace Prototype.Vibration
{
    [CustomEditor(typeof(Vibro))]
    public class VibroEditor : Editor
    {
        private Vibro target;

        private SerializedProperty patternProperty;

        private SerializedProperty amplitudesProperty;

        private void OnEnable()
        {
            target = (Vibro)base.target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (target == null)
                target = (Vibro)base.target;

            patternProperty = serializedObject.FindProperty("pattern");

            amplitudesProperty = serializedObject.FindProperty("amplitudes");

            Draw();

            serializedObject.ApplyModifiedProperties();
        }

        private void Draw()
        {
            GUILayout.BeginVertical("box");
            {
                target.type = (TVibration)EditorGUILayout.EnumPopup("Vibration type (own or created):", target.type);

                if (target.type == TVibration.Common)
                {
                    target.hapticTypes = (HapticTypes)EditorGUILayout.EnumPopup("The type of vibration created:", target.hapticTypes);
                }
                else
                {
                    EditorGUILayout.PropertyField(patternProperty, new GUIContent("Pattern: "), true);

                    EditorGUILayout.PropertyField(amplitudesProperty, new GUIContent("Amplitudes: "), true);
                }

            }
            GUILayout.EndVertical();
        }
    }
}
