using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;

namespace SBabchuk.Databases
{
    public class SettingsDrawer
    {
        static int selected = 0;

        private static Color defaultColor;

        private static PlaneSettings database;

        public static void Draw(PlaneSettings _database, int selectedMode)
        {
            if (database == null)
                database = _database;

            defaultColor = GUI.color;
            GUI.color = Color.grey;

            GUILayout.BeginVertical("box");
            {
                DrawInfo(_database);
            }
            GUILayout.EndVertical();
        }

        private static void DrawInfo(PlaneSettings _database)
        {
            GUI.color = Color.grey;
            GUILayout.BeginVertical("box");
            {
                DrawSettings();
            }
            GUILayout.EndVertical();
            GUI.color = defaultColor;
        }
        
        private static void DrawSettings()
        {
            GUI.color = defaultColor;
            EditorGUILayout.LabelField("Настройка літака:");
            
            GUI.color = defaultColor;
            GUILayout.BeginVertical("box");
            {
                var title = "Air Glid (Дальність польоту)";
                Utils.CheckColor(database.airGlid, 0);
                database.airGlid = EditorGUILayout.FloatField(title, database.airGlid);
                Utils.ChangeColor(defaultColor);
                
                title = "Handling (Радіус повороту)";
                Utils.CheckColor(database.handling, 0);
                database.handling = EditorGUILayout.FloatField(title, database.handling);
                Utils.ChangeColor(defaultColor);
                
                title = "Cost (Вартість)";
                Utils.CheckColor(database.cost, 0);
                database.cost = EditorGUILayout.IntField(title, database.cost);
                Utils.ChangeColor(defaultColor);
                
                title = "ResearchPoint (Очки вивчення)";
                Utils.CheckColor(database.researchPoint, 0);
                database.researchPoint = EditorGUILayout.IntField(title, database.researchPoint);
                Utils.ChangeColor(defaultColor);
                
                title = "DetailsCount (К-сть деталей)";
                Utils.CheckColor(database.detailsCount, 0);
                database.detailsCount = EditorGUILayout.IntField(title, database.detailsCount);
                Utils.ChangeColor(defaultColor);
                
                title = "Color (Колір)";
                Utils.CheckColor((int)database.color, 0);
                database.color = (PlaneColors)EditorGUILayout.EnumPopup(title, database.color);
                GUI.color = defaultColor;
            }
            GUILayout.EndVertical();
        }
    }
}
