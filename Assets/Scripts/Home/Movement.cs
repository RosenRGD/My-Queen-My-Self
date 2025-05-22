using System;
using System.Collections.Generic;
using MyQueenMySelf.Input;
using MyQueenMySelf.Utils;
using UnityEngine;

namespace MyQueenMySelf.Home
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        [SerializeField] GameObject _ground;
        [SerializeField] float _speed = 1f;

        Animator _animator;
        Rigidbody2D _rb;
        Vector2 _moveDirection = new Vector2(0, 0);
        Obstacle[] _obstacles;

        Interactable _currentInteractable = null;

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            _obstacles = FindObjectsByType<Obstacle>(FindObjectsSortMode.None);
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

        void FixedUpdate()
        {
            if (_moveDirection.x != 0 || _moveDirection.y != 0)
            {
                Vector2 amountToMove = _moveDirection * (_speed * Time.fixedDeltaTime);

                Vector2 targetPosition = _rb.position + amountToMove;

                _rb.MovePosition(targetPosition);
                // bool isBlocked = Physics2D.OverlapCircle(targetPosition, collisionRadius, obstacleLayer);

                // if (!isBlocked)
                // {
                //     transform.position = targetPosition;
                // }
            }
        }

        void HandleMove(Vector2 direction)
        {
            _moveDirection = direction;
            UpdateAnimation(direction);
        }

        void UpdateAnimation(Vector2 direction)
        {
            if (Mathf.Abs(direction.y) > 0)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isHorizontal", false);

                Vector3 scale = _animator.transform.localScale;

                scale.x = 1;
                if (direction.y > 0)
                {
                    scale.y = 1;
                }
                else if (direction.y < 0)
                {
                    scale.y = -1;
                }

                _animator.transform.localScale = scale;
            }
            else if (Mathf.Abs(direction.x) > 0)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isHorizontal", true);

                Vector3 scale = _animator.transform.localScale;

                scale.y = 1;
                if (direction.x > 0)
                {
                    scale.x = 1;
                }
                else if (direction.x < 0)
                {
                    scale.x = -1;
                }

                _animator.transform.localScale = scale;
            }
            else
            {
                _animator.SetBool("isWalking", false);
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
            if (interactable == _currentInteractable && interactable)
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
