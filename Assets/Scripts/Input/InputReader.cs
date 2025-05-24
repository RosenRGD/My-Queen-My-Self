using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyQueenMySelf.Input
{
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlanetActions, GameInput.IUIActions, GameInput.IDialogueActions
    {
        GameInput _gameInput;

        void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();

                _gameInput.Planet.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);
                _gameInput.Dialogue.SetCallbacks(this);
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

                _gameInput.Dialogue.Disable();
                _gameInput.Dialogue.SetCallbacks(null);
            }
        }

        public void SetGameplay()
        {
            _gameInput.Dialogue.Disable();
            _gameInput.UI.Disable();
            _gameInput.Planet.Enable();
        }

        public void SetUI()
        {
            _gameInput.Dialogue.Disable();
            _gameInput.UI.Enable();
            _gameInput.Planet.Disable();
        }

        public void SetDialogue()
        {
            _gameInput.Dialogue.Enable();
            _gameInput.UI.Disable();
            _gameInput.Planet.Disable();
        }

        public event Action<Vector2> MoveEvent;
        public event Action InteractEvent;
        public event Action InteractCancelledEvent;
        public event Action PauseGameplayEvent;

        public event Action PauseDialogueEvent;
        public event Action ProceedEvent;

        public event Action ResumeEvent;


        public void OnMove(InputAction.CallbackContext context)
        {
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

        public void OnPauseGameplay(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseGameplayEvent?.Invoke();
                SetUI();
            }
        }

        public void OnPauseDialogue(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseDialogueEvent?.Invoke();
                SetUI();
            }
        }

        public void OnProceed(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                ProceedEvent?.Invoke();
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
