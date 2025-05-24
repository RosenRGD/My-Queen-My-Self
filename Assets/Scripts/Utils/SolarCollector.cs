using UnityEngine;

namespace MyQueenMySelf.Utils
{
    [RequireComponent(typeof(Interactable))]
    public class SolarCollector : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
			GameManager.Instance.GatherSolar();
        }
    }
}
