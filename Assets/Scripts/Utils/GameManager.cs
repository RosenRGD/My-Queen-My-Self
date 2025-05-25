using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        DialogueManager _dialogueManager;
        [SerializeField] Dialogue _alreadyGardenedDialogue;
        [SerializeField] Dialogue _alreadyGatheredSolarDialogue;
        [SerializeField] Dialogue _alreadyDepositedSolarDialogue;
        [SerializeField] Dialogue _alreadyWatchedTVDialogue;
        [SerializeField] Dialogue _iNeedGatherSolar;
        [SerializeField] Dialogue _iNeedWorkDone;
        [SerializeField] Dialogue _iNeedWatchTV;

        [SerializeField] List<Dialogue> _queenRecordings;
        [SerializeField] List<Dialogue> _startDialogue;

        bool hadFirstDialogue = false;
        bool isInDream = false;
        public bool IsInDream
        {
            get { return isInDream; }
        }

        bool[] _successes = new bool[8];
        [SerializeField] int _currentDay = 1;

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

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _dialogueManager = FindFirstObjectByType<DialogueManager>();
            if (!hadFirstDialogue && !isInDream)
            {
                _dialogueManager.StartDialogue(_startDialogue[_currentDay - 1]);
                hadFirstDialogue = true;
            }
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
                _dialogueManager.StartDialogue(_alreadyGardenedDialogue);
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
                _dialogueManager.StartDialogue(_alreadyGatheredSolarDialogue);
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
            else if (_todoItems[TodoItemName.DepositSolar].IsCompleted)
            {
                _dialogueManager.StartDialogue(_alreadyDepositedSolarDialogue);
                return false;
            }
            else
            {
                _dialogueManager.StartDialogue(_iNeedGatherSolar);
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
                _dialogueManager.StartQueenDialogue(_queenRecordings[_currentDay - 1]);
                return true;
            }
            else if (_todoItems[TodoItemName.DepositSolar].IsCompleted)
            {
                _dialogueManager.StartDialogue(_alreadyWatchedTVDialogue);
                return false;
            }
            else
            {
                _dialogueManager.StartDialogue(_iNeedWorkDone);
                return false;
            }
        }

        public bool Sleep()
        {
            if (_todoItems[TodoItemName.Garden].IsCompleted &&
                _todoItems[TodoItemName.CollectSolar].IsCompleted &&
                _todoItems[TodoItemName.DepositSolar].IsCompleted &&
                _todoItems[TodoItemName.WatchTV].IsCompleted)
            {
                _todoItems[TodoItemName.Sleep].IsCompleted = true;
                OnTodoListUpdated?.Invoke();
                StartDream();
                return true;
            }
            else
            {
                _dialogueManager.StartDialogue(_iNeedWatchTV);
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

        void StartDream()
        {
            isInDream = true;
            SceneLoader.Instance.LoadDreamScene(_currentDay);
        }

        public void EndDream(bool isSuccess)
        {
            _successes[_currentDay - 1] = isSuccess;
            _currentDay += 1;
            if (_currentDay < 8)
            {
                ResetForNewDay();
                SceneLoader.Instance.LoadHomeScene();
            }
            else
            {
                ManageEndScene();
            }
        }

        void ResetForNewDay()
        {
            isInDream = false;
            hadFirstDialogue = false;
            foreach (KeyValuePair<TodoItemName, TodoItem> entry in _todoItems)
            {
                entry.Value.IsCompleted = false;
            }
        }

        void ManageEndScene()
        {
            int amountOfWins = 0;
            foreach (bool win in _successes)
            {
                if (win)
                {
                    amountOfWins += 1;
                }
                else
                {
                    amountOfWins += 1;
                }
            }

            if (amountOfWins >= 7)
            {

            }
            else
            {

            }
        }

        void LoadWinScene()
        {

        }

        void LoadFailScene()
        {
            
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
