using UnityEngine;

namespace MyQueenMySelf.Utils
{
    [RequireComponent(typeof(Interactable))]
    public class Garden : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
			GameManager.Instance.HarvestGarden();
        }
    }
}
