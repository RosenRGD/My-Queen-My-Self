using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyQueenMySelf.Utils
{
    public class GameManager : MonoBehaviour
    {

        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (_isShuttingDown) return null;

                if (instance == null)
                {
                    instance = FindFirstObjectByType<GameManager>();

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(GameManager).ToString());
                        instance = singleton.AddComponent<GameManager>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return instance;
            }
        }

        private static bool _isShuttingDown = false;

        SceneLoader _sceneLoader;

        bool hasGardened = false;
        bool hasGatheredSolar = false;

        enum TodoItemName
        {
            Garden,
            CollectSolar,
            DepositSolar,
            WatchTV,
            Sleep
        }

        Dictionary<TodoItemName, TodoItem> _todoItems = new Dictionary<TodoItemName, TodoItem>();
        public Action OnTodoListUpdated;

        void Awake()
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

            _sceneLoader = GetComponent<SceneLoader>();

            _todoItems[TodoItemName.Garden] = new TodoItem("Harvest/Plant Garden");
            _todoItems[TodoItemName.CollectSolar] = new TodoItem("Collect the solar energy");
            _todoItems[TodoItemName.DepositSolar] = new TodoItem("Deposit the solar energy");
            _todoItems[TodoItemName.WatchTV] = new TodoItem("Watch Her Majesty's recordings");
            _todoItems[TodoItemName.Sleep] = new TodoItem("Sleep");
        }

        void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }

        public bool HarvestGarden()
        {
            if (!_todoItems[TodoItemName.Garden].IsCompleted)
            {
                _todoItems[TodoItemName.Garden].IsCompleted = true;
                OnTodoListUpdated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GatherSolar()
        {
            if (!_todoItems[TodoItemName.CollectSolar].IsCompleted)
            {
                _todoItems[TodoItemName.CollectSolar].IsCompleted = true;
                OnTodoListUpdated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DepositSolar()
        {
            if (!_todoItems[TodoItemName.DepositSolar].IsCompleted && _todoItems[TodoItemName.CollectSolar].IsCompleted)
            {
                _todoItems[TodoItemName.DepositSolar].IsCompleted = true;
                OnTodoListUpdated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WatchTV()
        {
            if (!_todoItems[TodoItemName.WatchTV].IsCompleted &&
                _todoItems[TodoItemName.Garden].IsCompleted &&
                _todoItems[TodoItemName.CollectSolar].IsCompleted &&
                _todoItems[TodoItemName.DepositSolar].IsCompleted)
            {
                _todoItems[TodoItemName.WatchTV].IsCompleted = true;
                OnTodoListUpdated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Sleep()
        {
            if (!_todoItems[TodoItemName.Sleep].IsCompleted &&
                _todoItems[TodoItemName.Garden].IsCompleted &&
                _todoItems[TodoItemName.CollectSolar].IsCompleted &&
                _todoItems[TodoItemName.DepositSolar].IsCompleted &&
                _todoItems[TodoItemName.WatchTV].IsCompleted)
            {
                _todoItems[TodoItemName.Sleep].IsCompleted = true;
                OnTodoListUpdated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TodoItem> GetTodoItems()
        {
            List<TodoItem> todoItems = new();
            foreach (TodoItemName todoItemName in Enum.GetValues(typeof(TodoItemName)))
            {
                todoItems.Add(_todoItems[todoItemName]);
            }
            return todoItems;
        }
    }

    public class TodoItem
    {
        public bool IsCompleted
        {
            get;
            set;
        }

        public string Name
        {
            get;
        }

        public TodoItem(string name)
        {
            Name = name;
            IsCompleted = false;
        }
    }
}
