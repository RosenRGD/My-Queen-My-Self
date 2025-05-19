using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyQueenMySelf.Input
{
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlanetActions, GameInput.IUIActions
    {
        GameInput _gameInput;

        void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();

                _gameInput.Planet.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);
            }

            SetGameplay();
        }

        void OnDisable()
        {
            if (_gameInput != null)
            {
                _gameInput.Planet.Disable();
                _gameInput.Planet.SetCallbacks(null);

                _gameInput.UI.Disable();
                _gameInput.UI.SetCallbacks(null);
            }
        }

        public void SetGameplay()
        {
            _gameInput.UI.Disable();
            _gameInput.Planet.Enable();
        }

        public void SetUI()
        {
            _gameInput.UI.Enable();
            _gameInput.Planet.Disable();
        }

        public event Action<Vector2> MoveEvent;

        public event Action InteractEvent;
        public event Action InteractCancelledEvent;

        public event Action PauseEvent;
        public event Action ResumeEvent;


        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log($"Phase: {context.phase}, value: {context.ReadValue<Vector2>()}");
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                InteractEvent?.Invoke();
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                InteractCancelledEvent?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUI();
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                ResumeEvent?.Invoke();
                SetGameplay();
            }
        }
    }
}
