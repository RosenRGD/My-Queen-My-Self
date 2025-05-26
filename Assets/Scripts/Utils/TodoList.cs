using UnityEngine;

namespace MyQueenMySelf.Utils
{
	public class TodoList : MonoBehaviour
	{
		void Awake()
		{
			if (GameManager.Instance.IsInWin || GameManager.Instance.IsInFail)
			{
				gameObject.SetActive(false);
			}
        }
    }
}
