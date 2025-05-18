using System;
using UnityEngine;

namespace MyQueenMySelf.Planet
{
    public class AntPlanetMovement : MonoBehaviour
    {
        [SerializeField] GameObject _planet;
        [SerializeField] int _radius;

        [SerializeField] float _currentAngle = 0.0f;

        float _angleOffset = ((float)Math.PI) * (1.0f/2.0f);

        void Update()
        {
            float x = (_radius * Mathf.Cos(_currentAngle + _angleOffset)) + _planet.transform.position.x;
            float y = (_radius * Mathf.Sin(_currentAngle + _angleOffset)) + _planet.transform.position.y;
            transform.position = new Vector2(x, y);

            transform.rotation = Quaternion.Euler(0, 0, _currentAngle * Mathf.Rad2Deg);

            if (_currentAngle > 2 * Math.PI || _currentAngle < -2 * Math.PI)
            {
                _currentAngle = _currentAngle % (2 * Mathf.PI);
                if (_currentAngle < 0)
                {
                    _currentAngle += 2 * Mathf.PI;
                }
            }
        }
    }
}

