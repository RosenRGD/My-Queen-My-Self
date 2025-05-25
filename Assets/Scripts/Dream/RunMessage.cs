using System.Collections;
using UnityEngine;

namespace MyQueenMySelf.Dream
{
	public class RunMessage : MonoBehaviour
	{
		[SerializeField] float timeToDisplay = 2;

		void Start()
		{
			StartCoroutine(WaitingToStop());
		}

		IEnumerator WaitingToStop()
		{
			yield return new WaitForSeconds(timeToDisplay);
			gameObject.SetActive(false);
		}
    }
}
