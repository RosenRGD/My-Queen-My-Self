using System;
using MyQueenMySelf.Input;
using UnityEngine;

namespace MyQueenMySelf.Home
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        [SerializeField] GameObject _ground;
        [SerializeField] float _speed = 1f;

        Animator _animator;
        Vector2 _moveDirection = new Vector2(0, 0);

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            _inputReader.MoveEvent += HandleMove;
        }

        

        void Update()
        {
            if (_moveDirection.x != 0 || _moveDirection.y != 0)
            {
                Vector2 amountToMove = _moveDirection * _speed * Time.deltaTime;

                transform.position = new Vector3(transform.position.x + amountToMove.x, transform.position.y + amountToMove.y, transform.position.z);
            }
            
        }

        void HandleMove(Vector2 direction)
        {
            _moveDirection = direction;

            if (Mathf.Abs(direction.y) > 0)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isHorizontal", false);
                if (direction.y >= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
            else if (Mathf.Abs(direction.x) > 0)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isHorizontal", true);
                if (direction.x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }
    }
}
