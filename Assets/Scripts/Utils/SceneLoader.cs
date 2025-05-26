using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyQueenMySelf.Utils
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader instance;

        public static SceneLoader Instance
        {
            get
            {
                if (_isShuttingDown) return null;

                if (instance == null)
                {
                    instance = FindFirstObjectByType<SceneLoader>();

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(SceneLoader).ToString());
                        instance = singleton.AddComponent<SceneLoader>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return instance;
            }
        }

        private static bool _isShuttingDown = false;


        private void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }


        [SerializeField] string _planetScene;
        [SerializeField] string _homeScene;
        [SerializeField] List<string> _dreamScenes;
        [SerializeField] string _winScene;
        [SerializeField] string _failScene;
        [SerializeField] string _menuScene;


        public void LoadPlanetScene()
        {
            SceneManager.LoadScene(_planetScene);
        }

        public void LoadHomeScene()
        {
            SceneManager.LoadScene(_homeScene);
        }

        public void LoadDreamScene(int currentDay)
        {
            SceneManager.LoadScene(_dreamScenes[currentDay - 1]);
        }

        public void LoadFailScene()
        {
            SceneManager.LoadScene(_failScene);
        }

        public void LoadWinScene()
        {
            SceneManager.LoadScene(_winScene);
        }

        internal void LoadMenuScene()
        {
            SceneManager.LoadScene(_menuScene);
        }
    }
}

