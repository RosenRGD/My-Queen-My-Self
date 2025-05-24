using System.Collections.Generic;
using MyQueenMySelf.Utils;
using TMPro;
using UnityEngine;

namespace MyQueenMySelf.UI
{
	public class TodoListManager : MonoBehaviour
	{
		[SerializeField] GameObject _textPrefab;

		List<GameObject> _todoItems = new();

		void Start()
		{
			UpdateTodoList();
		}

		void OnEnable()
		{
			GameManager.Instance.OnTodoListUpdated += UpdateTodoList;
		}

		void OnDisable()
		{
			GameManager.Instance.OnTodoListUpdated -= UpdateTodoList;
		}

		void UpdateTodoList()
		{
			ClearTodoList();
			foreach (TodoItem todoItem in GameManager.Instance.GetTodoItems())
			{
				GameObject textGameObject = Instantiate(_textPrefab, transform);
				TextMeshProUGUI text = textGameObject.GetComponentInChildren<TextMeshProUGUI>();

				if (todoItem.IsCompleted)
				{
					text.text = "<s>" + todoItem.Name + "</s>";
				}
				else
				{
					text.text = todoItem.Name;
				}
				_todoItems.Add(textGameObject);
			}
		}

		void ClearTodoList()
		{
			foreach (GameObject child in _todoItems)
			{
				Destroy(child);
			}
		}

    }
}
