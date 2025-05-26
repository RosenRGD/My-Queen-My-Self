using MyQueenMySelf.Utils;
using UnityEngine;

namespace MyQueenMySelf.UI
{
	public class PlayButton : MonoBehaviour
	{
		public void Play()
		{
			GameManager.Instance.PlayNewGame();
		}
	}
}
