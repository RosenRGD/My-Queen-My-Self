using UnityEngine;

namespace MyQueenMySelf.Utils
{
    public class GameManager : MonoBehaviour
    {
        SceneLoader _sceneLoader;
        void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
        }


    }
}
