using System;
using System.Collections;
using MyQueenMySelf.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyQueenMySelf.Utils
{
	public class DialogueManager : MonoBehaviour
	{
		private static DialogueManager instance;

		public static DialogueManager Instance
		{
			get
			{
				if (_isShuttingDown) return null;

				if (instance == null)
				{
					instance = FindFirstObjectByType<DialogueManager>();

					if (instance == null)
					{
						GameObject singleton = new GameObject(typeof(DialogueManager).ToString());
						instance = singleton.AddComponent<DialogueManager>();
						DontDestroyOnLoad(singleton);
					}
				}
				return instance;
			}
		}

		private static bool _isShuttingDown = false;

		[SerializeField] Image _namePanel;
		[SerializeField] Image _textPanel;
		[SerializeField] InputReader _inputReader;
		[SerializeField] float _timeBetweenCharacterAppearing;

		TextMeshProUGUI _nameText;
		TextMeshProUGUI _lineText;

		bool _hasProceeded = false;


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

			_nameText = _namePanel.GetComponentInChildren<TextMeshProUGUI>();
			_lineText = _textPanel.GetComponentInChildren<TextMeshProUGUI>();
		}

		void OnApplicationQuit()
		{
			_isShuttingDown = true;
		}

		void OnEnable()
		{
			_inputReader.ProceedEvent += UpdateHasProceeded;
        }

        void OnDisable()
        {
            _inputReader.ProceedEvent -= UpdateHasProceeded;
        }

        public void StartDialogue(Dialogue dialogue)
		{
			StartCoroutine(PlayDialogue(dialogue));
		}

		IEnumerator PlayDialogue(Dialogue dialogue)
		{
			_inputReader.SetDialogue();
			foreach (Line line in dialogue.GetAllLines())
			{
				yield return DisplayLine(line);
				while (!_hasProceeded)
				{
					yield return null;
				}
				_hasProceeded = false;
			}
			_inputReader.SetGameplay();
		}

		IEnumerator DisplayLine(Line line)
		{
			_nameText.text = line.Speaker;
			int currentIndexInLine = 0;
			while (currentIndexInLine < line.Text.Length)
			{
				if (_hasProceeded)
				{
					_lineText.text = line.Text;
					currentIndexInLine = line.Text.Length;
					_hasProceeded = false;
				}
				else
				{
					_lineText.text = line.Text.Substring(0, currentIndexInLine + 1);
					currentIndexInLine += 1;
				}
				yield return new WaitForSeconds(_timeBetweenCharacterAppearing);
			}
		}

		void UpdateHasProceeded()
		{
			_hasProceeded = true;
		}
    }
}
