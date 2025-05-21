using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyQueenMySelf.Utils
{
    public class SceneLoader : MonoBehaviour
    {
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

