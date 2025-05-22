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

        
        [SerializeField] string planetScene;
        [SerializeField] string homeScene;
        [SerializeField] List<string> dreamScenes;


        public void LoadPlanetScene()
        {
            SceneManager.LoadScene(planetScene);
        }

        public void LoadHomeScene()
        {   
            SceneManager.LoadScene(homeScene);
        } 
    }
}

