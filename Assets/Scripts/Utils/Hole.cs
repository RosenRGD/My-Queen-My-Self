using UnityEngine;

namespace MyQueenMySelf.Utils
{
	[RequireComponent(typeof(Interactable))]
    public class Hole : MonoBehaviour, IInteractable
	{
		public void Interact()
		{
			GameManager.Instance.EndDream(true);
		}
	}
}
