using MyQueenMySelf.Utils;
using UnityEngine;

namespace MyQueenMySelf.Home
{
	public class PlayerInteract : MonoBehaviour
	{
		Interactable _currentInteractable = null;
		void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("ENter");
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable)
            {
                _currentInteractable.OpenTooltip();
                _currentInteractable = interactable;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Exit");
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable == _currentInteractable)
            {
                _currentInteractable.CloseTooltip();
                _currentInteractable = null;

            }
        }
	}
}
