using Asteroids.Datas;
using System;
using UnityEditor;
using UnityEngine;

namespace Asteroids.EditorInspectors
{
    [CustomEditor(typeof(AsteroidsLevelData))]
    [CanEditMultipleObjects]
    public class AsteroidsLevelDataInspector : Editor
    {
        GUIStyle titlesStyle;
        AsteroidsLevelData asteroidsLevelData = null;

        protected void OnEnable()
        {
            asteroidsLevelData = (AsteroidsLevelData)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            titlesStyle = new GUIStyle(GUI.skin.label);
            titlesStyle.fontSize = 13;
            titlesStyle.fontStyle = FontStyle.Bold;

            ShowAsteroids(asteroidsLevelData.AsteroidsSpawnInfo);
            AddRemoveAsteroids(ref asteroidsLevelData.AsteroidsSpawnInfo);

            bool somethingChanged = EditorGUI.EndChangeCheck();
            if (somethingChanged)
            {
                EditorUtility.SetDirty(asteroidsLevelData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void ShowAsteroids(AsteroidSpawnInfo[] asteroidsSpawnInfo)
        {
            for (int i = 0; i < asteroidsLevelData.AsteroidsSpawnInfo.Length; i++)
            {
                EditorGUILayout.LabelField((i + 1).ToString() + ". Asteroid: ", titlesStyle);
                DrawSpawnInfoField(asteroidsLevelData.AsteroidsSpawnInfo[i]);
                EditorGUIUtility.labelWidth = 0;
            }
        }

        private void AddRemoveAsteroids(ref AsteroidSpawnInfo[] asteroidsSpawnInfo)
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
            {
                Array.Resize(ref asteroidsSpawnInfo, asteroidsSpawnInfo.Length + 1);
            }

            if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)))
            {
                if (asteroidsSpawnInfo.Length > 0)
                {
                    Array.Resize(ref asteroidsSpawnInfo, asteroidsSpawnInfo.Length - 1);
                }
            }

            GUILayout.EndHorizontal();
        }

        private void DrawSpawnInfoField(AsteroidSpawnInfo asteroidSpawnInfo)
        {
            asteroidSpawnInfo.ViewportPosition = EditorGUILayout.Vector2Field("Viewport position :", 
                                                 new Vector2(Mathf.Clamp(asteroidSpawnInfo.ViewportPosition.x, 0, 1), 
                                                 Mathf.Clamp(asteroidSpawnInfo.ViewportPosition.y, 0, 1)));

            Vector2 direction = EditorGUILayout.Vector2Field("Direction :",
                                     new Vector2(Mathf.Clamp(asteroidSpawnInfo.Direction.x, -1, 1),
                                     Mathf.Clamp(asteroidSpawnInfo.Direction.z, -1, 1)));
            asteroidSpawnInfo.Direction = new Vector3(direction.x, 0, direction.y);
        }


    }
}