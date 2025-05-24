using UnityEngine;

namespace MyQueenMySelf.Utils
{
    [RequireComponent(typeof(Interactable))]
    public class SolarDeposit : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
			GameManager.Instance.DepositSolar();
        }
    }
}
