using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Asteroids.Tools
{
    public class AsteroidsCopierWindow : EditorWindow
    {
        private static AsteroidsCopierWindow WINDOW;
        private static float FIELD_HEIGHT = 23;

        private Vector2 windowSize;

        private GUIStyle annotationsStyle;
        private GUIStyle titlesStyle;

        private Transform asreoidsToCopyParent;
        private Transform asreoidsToPasteParent;
        private List<GameObject> asteroidsPrefabs;

        // Add a menu item named "Do Something" to MyMenu in the menu bar.
        [MenuItem("Asteroids/Tools/Asteroids copier")]
        private static void CreateWindowItem()
        {
            Vector2 size = new Vector2(310, 310);
            WINDOW = ScriptableObject.CreateInstance<AsteroidsCopierWindow>();
            WINDOW.position = new Rect(Screen.width / 2, Screen.height / 2, size.x, size.y);
            WINDOW.minSize = size;
            WINDOW.maxSize = size;
            WINDOW.name = "Asteroids copier";
            WINDOW.ShowUtility();
        }

        private void OnGUI()
        {
            if (asteroidsPrefabs == null)
            {
                Setup();
            }

            EditorGUILayout.LabelField("Asteroids copier", titlesStyle);
            EditorGUILayout.LabelField("Creastes random asteroids using a list of prefabs", annotationsStyle);
            EditorGUILayout.LabelField("and the transforms of the previous asteroids", annotationsStyle);
            GUILayout.Space(5);

            SelectAsteroidsParents();
            SelectAsteroidsPrefabs();
            AddRemoveAsteroids();

            if (asreoidsToPasteParent && asreoidsToCopyParent && asteroidsPrefabs.Count > 0)
            {
                EnableButton();
            }
        }
        public void Setup()
        {
            InitStyles();
            asteroidsPrefabs = new List<GameObject>();
            windowSize = WINDOW.minSize;
        }

        private void CopyRandomAsteroids()
        {
            foreach (Transform asteroidToCopy in asreoidsToCopyParent)
            {
                Transform asteroidTr = InstantiateRandAsteroid();
                asteroidTr.parent = asreoidsToPasteParent;

                asteroidTr.localPosition = asteroidToCopy.localPosition;
                asteroidTr.localRotation = asteroidToCopy.localRotation;
                asteroidTr.localScale = asteroidToCopy.localScale;
            }

            asreoidsToCopyParent.gameObject.SetActive(false);
        }

        private Transform InstantiateRandAsteroid()
        {
            int randNum = UnityEngine.Random.Range(0, asteroidsPrefabs.Count);
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(asteroidsPrefabs[randNum]);
            return go.transform;
        }

        private void SelectAsteroidsPrefabs()
        {
            EditorGUILayout.LabelField("Please, select the asteroids prefabs to copy : ", EditorStyles.wordWrappedLabel);
            GUILayout.Space(5);
            for (int i = 0; i < asteroidsPrefabs.Count; i++)
            {
                asteroidsPrefabs[i] = (GameObject)EditorGUILayout.ObjectField(asteroidsPrefabs[i], typeof(GameObject), true, GUILayout.Width(200), GUILayout.Height(20));
            }
            GUILayout.Space(5);
        }

        private void SelectAsteroidsParents()
        {
            EditorGUILayout.LabelField("Please, select the asteroids´parent used to copy: ", EditorStyles.wordWrappedLabel);
            GUILayout.Space(5);
            asreoidsToCopyParent = (Transform)EditorGUILayout.ObjectField(asreoidsToCopyParent, typeof(Transform), true, GUILayout.Width(200), GUILayout.Height(20));
            GUILayout.Space(5);

            EditorGUILayout.LabelField("Please, select the parent go to paste the asteroids : ", EditorStyles.wordWrappedLabel);
            GUILayout.Space(5);
            asreoidsToPasteParent = (Transform)EditorGUILayout.ObjectField(asreoidsToPasteParent, typeof(Transform), true, GUILayout.Width(200), GUILayout.Height(20));
            GUILayout.Space(5);
        }

        private void EnableButton()
        {
            GUILayout.Space(5);
            if (GUILayout.Button("Copy asteroids", GUILayout.Width(295), GUILayout.Height(50)))
            {
                CopyRandomAsteroids();
            }
        }

        private void AddRemoveAsteroids()
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
            {
                asteroidsPrefabs.Add(null);

                windowSize.y += FIELD_HEIGHT;
                WINDOW.minSize = windowSize;
                WINDOW.maxSize = windowSize;
            }

            if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)))
            {
                if (asteroidsPrefabs.Count > 0)
                {
                    asteroidsPrefabs.RemoveAt(asteroidsPrefabs.Count - 1);

                    windowSize.y -= FIELD_HEIGHT;
                    WINDOW.minSize = windowSize;
                    WINDOW.maxSize = windowSize;
                }
            }

            GUILayout.EndHorizontal();
        }
        private void InitStyles()
        {
            annotationsStyle = new GUIStyle(GUI.skin.label);
            annotationsStyle.fontSize = 10;
            annotationsStyle.fontStyle = FontStyle.Italic;
            annotationsStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f);

            titlesStyle = new GUIStyle(GUI.skin.label);
            titlesStyle.fontSize = 12;
            titlesStyle.fontStyle = FontStyle.Bold;
        }
    }
}