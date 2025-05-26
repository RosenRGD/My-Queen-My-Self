using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Make sure this is included

namespace MyQueenMySelf.Ending
{
    public class FadeInFadeOut : MonoBehaviour
    {
        [SerializeField] float fadeInSpeed = 1f;
        [SerializeField] float fadeOutSpeed = 1f;

        Image _image;

        void Awake()
        {
            _image = GetComponent<Image>();

            if (_image != null)
            {
                Color color = _image.color;
                color.a = 1f;
                _image.color = color;
            }
        }

        public IEnumerator FadingIn()
        {
            if (_image == null)
				yield break;

            Color color = _image.color;
			color.a = 0f;
            _image.color = color;

            while (color.a < 1f)
			{
				color.a += fadeInSpeed * Time.deltaTime;
				_image.color = color;
				yield return null;
			}

            color.a = 1f;
            _image.color = color;
        }

        public IEnumerator FadingOut()
        {
            if (_image == null)
                yield break;

            Color color = _image.color;
			color.a = 1f;
            _image.color = color;

            while (color.a > 0f)
			{
				color.a -= fadeOutSpeed * Time.deltaTime;
				_image.color = color;
				yield return null;
			}

            color.a = 0f;
            _image.color = color;
        }
    }
}
