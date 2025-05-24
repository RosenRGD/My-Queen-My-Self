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
		[SerializeField] Image _namePanel;
		[SerializeField] Image _textPanel;

		[SerializeField] Image _tvImage;

		[SerializeField] InputReader _inputReader;
		[SerializeField] float _timeBetweenCharacterAppearing;

		TextMeshProUGUI _nameText;
		TextMeshProUGUI _lineText;

		bool _hasProceeded = false;


		void Awake()
		{
			_nameText = _namePanel.GetComponentInChildren<TextMeshProUGUI>();
			_lineText = _textPanel.GetComponentInChildren<TextMeshProUGUI>();

			_namePanel.gameObject.SetActive(false);
			_textPanel.gameObject.SetActive(false);
 
			_tvImage.gameObject.SetActive(false);
		}



        void OnEnable()
		{
			_inputReader.ProceedEvent += UpdateHasProceeded;
        }

        void OnDisable()
        {
            _inputReader.ProceedEvent -= UpdateHasProceeded;
        }

		public void StartQueenDialogue(Dialogue dialogue)
		{
			StartCoroutine(PlayQueenDialogue(dialogue));
		}

        public void StartDialogue(Dialogue dialogue)
		{
			StartCoroutine(PlayDialogue(dialogue));
		}

		IEnumerator PlayQueenDialogue(Dialogue dialogue)
		{
			_tvImage.gameObject.SetActive(true);
			yield return PlayDialogue(dialogue);
			_tvImage.gameObject.SetActive(false);
		}

		IEnumerator PlayDialogue(Dialogue dialogue)
		{
			_inputReader.SetDialogue();
			_namePanel.gameObject.SetActive(true);
			_textPanel.gameObject.SetActive(true);
			foreach (Line line in dialogue.GetAllLines())
			{
				yield return DisplayLine(line);
				while (!_hasProceeded)
				{
					yield return null;
				}
				_hasProceeded = false;
			}
			_namePanel.gameObject.SetActive(false);
			_textPanel.gameObject.SetActive(false);
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
