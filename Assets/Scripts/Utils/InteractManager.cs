using System;
using UnityEngine;

namespace MyQueenMySelf.Utils
{
    public class InteractManager : MonoBehaviour
    {
        private static InteractManager instance;
    
        public static InteractManager Instance
        {
            get
            {
                if (_isShuttingDown) return null;

                if (instance == null)
                {
                    instance = FindFirstObjectByType<InteractManager>();

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(InteractManager).ToString());
                        instance = singleton.AddComponent<InteractManager>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return instance;
            }
        }

        private static bool _isShuttingDown = false;



        public event Action<string> OnTooltipEnterEvent;
        public event Action OnTooltipExitEvent;

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

        private void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }

        public void TooltipEnter(string promptText)
        {
            OnTooltipEnterEvent?.Invoke(promptText);
        }

        public void TooltipExit()
        {
            OnTooltipExitEvent?.Invoke();
        }
    }
}