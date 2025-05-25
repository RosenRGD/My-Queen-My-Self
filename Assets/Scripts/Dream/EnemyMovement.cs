using MyQueenMySelf.Home;
using MyQueenMySelf.Utils;
using UnityEngine;

namespace MyQueenMySelf.Dream
{
	public class EnemyMovement : MonoBehaviour
	{
		[SerializeField] float speed = 1f;

		void Update()
		{
			float amountToMove = speed * Time.deltaTime;
			transform.position = new Vector3(0, transform.position.y + amountToMove, 0);
		}

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<Movement>())
			{
				GameManager.Instance.EndDream(false);
			}
        }
    }
}
