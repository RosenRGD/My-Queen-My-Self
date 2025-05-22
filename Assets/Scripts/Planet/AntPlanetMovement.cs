using System;
using MyQueenMySelf.Input;
using MyQueenMySelf.Utils;
using UnityEngine;

namespace MyQueenMySelf.Planet
{
    public class AntPlanetMovement : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        [SerializeField] GameObject _planet;
        [SerializeField] int _radius;
        [SerializeField] float _speed = 1f;

        Animator _animator;

        Interactable _currentInteractable = null;

        float _currentAngle = 0.0f;
        float _angleOffset = ((float)Math.PI) * (1.0f / 2.0f);
        float _currentMoveDirection = 0f;

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void OnEnable()
        {
            _inputReader.MoveEvent += HandleMove;
            _inputReader.InteractEvent += HandleInteract;
        }

        void OnDisable()
        {
            _inputReader.MoveEvent -= HandleMove;
            _inputReader.InteractEvent -= HandleInteract;
        }



        void Update()
        {
            _currentAngle += _currentMoveDirection * _speed * Time.deltaTime;

            float x = (_radius * Mathf.Cos(_currentAngle + _angleOffset)) + _planet.transform.position.x;
            float y = (_radius * Mathf.Sin(_currentAngle + _angleOffset)) + _planet.transform.position.y;
            transform.position = new Vector2(x, y);

            transform.rotation = Quaternion.Euler(0, 0, _currentAngle * Mathf.Rad2Deg);

            if (_currentAngle > 2 * Math.PI || _currentAngle < 0)
            {
                _currentAngle = _currentAngle % (2 * Mathf.PI);
                if (_currentAngle < 0)
                {
                    _currentAngle += 2 * Mathf.PI;
                }
            }
        }

        void HandleMove(Vector2 direction)
        {
            _currentMoveDirection = -direction.x;

            Vector3 scale = _animator.transform.localScale;
            if (_currentMoveDirection < 0)
            {
                scale.x = -1;
            }
            else if (_currentMoveDirection > 0)
            {
                scale.x = 1;
            }
            _animator.transform.localScale = scale;

            if (_currentMoveDirection == 0)
            {
                _animator.SetBool("isWalking", false);
            }
            else
            {
                _animator.SetBool("isWalking", true);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable)
            {
                interactable.OpenTooltip();
                _currentInteractable = interactable;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable == _currentInteractable)
            {
                _currentInteractable.CloseTooltip();
                _currentInteractable = null;
            }
        }

        void HandleInteract()
        {
            if (_currentInteractable != null)
            {
                MonoBehaviour[] components = _currentInteractable.GetComponents<MonoBehaviour>();

                foreach (MonoBehaviour comp in components)
                {
                    if (comp is IInteractable interactable)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}

