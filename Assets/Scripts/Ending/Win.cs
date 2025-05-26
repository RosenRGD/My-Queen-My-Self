using System.Collections;
using MyQueenMySelf.Utils;
using UnityEngine;

namespace MyQueenMySelf.Ending
{
	public class Win : MonoBehaviour
	{
		[SerializeField] Dialogue _endingDialogue;

		void Start()
		{
			StartCoroutine(PlayEnding());
		}

		IEnumerator PlayEnding()
		{
			FadeInFadeOut fadeInFadeOut = FindFirstObjectByType<FadeInFadeOut>();
			yield return fadeInFadeOut.FadingOut();
			DialogueManager dialogueManager = FindFirstObjectByType<DialogueManager>();
			yield return dialogueManager.PlayDialogue(_endingDialogue);
			yield return fadeInFadeOut.FadingIn();
		}
	}
}
