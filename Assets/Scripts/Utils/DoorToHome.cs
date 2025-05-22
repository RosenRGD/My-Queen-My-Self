using UnityEngine;

namespace MyQueenMySelf.Utils
{
    [RequireComponent(typeof(Interactable))]
    public class DoorToHome : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            SceneLoader.Instance.LoadHomeScene();
        }
    }
}

