using UnityEngine;

namespace MyQueenMySelf.Utils
{
    [RequireComponent(typeof(Interactable))]
    public class TV : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
			GameManager.Instance.WatchTV();
        }
    }
}
