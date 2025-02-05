using UnityEditor;
using UnityEngine;

namespace SBabchuk
{
    public class BaseDatabaseEditor : Editor
    {
        public ScriptableObject database;

        public int selectedMode = 0;

        static Color defaultColor;

        public void OnEnable()
        {
            database = (ScriptableObject)target;
        }

        public override void OnInspectorGUI()
        {
            defaultColor = GUI.color;

            if (database == null)
                database = (ScriptableObject)target;

            DrawButtonSave();

            Draw();

            Update();
        }

        public virtual void Draw()
        {

        }

        public void Update()
        {
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed) //SetObjectDirty(database);
                DrawButtonSave();
        }

        public void DrawButtonSave()
        {
            Utils.ChangeColor(Color.green);
            if (GUILayout.Button("Зберегти", GUILayout.Height(20)))
            {
                SetObjectDirty(database);

                SaveSO();

            }
            Utils.ChangeColor(defaultColor);
        }

        public virtual void SaveSO()
        {

        }

        public static void SetObjectDirty(Object obj)
        {
            EditorUtility.SetDirty(obj);
            AssetDatabase.SaveAssets();
        }
    }
}
