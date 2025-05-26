using System.Collections;
using UnityEngine;

namespace MyQueenMySelf.Utils
{
	[RequireComponent(typeof(Interactable))]
	public class Rocket : MonoBehaviour, IInteractable
	{
		[SerializeField] float _distanceAwayUntilEndScene = 240f;
		[SerializeField] float _speed = 5f;
		ParticleSystem _particle;

		void Awake()
		{
			ParticleSystem _particle = GetComponentInChildren<ParticleSystem>();
			_particle.Stop();
        }

        public void Interact()
		{
			if (GameManager.Instance.FlyAway())
			{
				StartCoroutine(FlyingAway());
			}
		}

		IEnumerator FlyingAway()
		{
			_particle = GetComponentInChildren<ParticleSystem>();
			_particle.Play();
			bool isFarAway = false;
			while (!isFarAway)
			{
				float amountToMove = _speed * Time.deltaTime;
				transform.position = new Vector3(transform.position.x - amountToMove, transform.position.y, 0);
				yield return null;

				if (Mathf.Abs(transform.position.x) > _distanceAwayUntilEndScene)
				{
					isFarAway = true;
				}
			}

			SceneLoader.Instance.LoadWinScene();
		}
    }
}
