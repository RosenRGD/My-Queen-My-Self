using UnityEngine;

namespace MyQueenMySelf.Utils
{
	public class Interactable : MonoBehaviour
	{
		[SerializeField] string tooltip;

		public void OpenTooltip()
		{
			InteractManager.Instance.TooltipEnter(tooltip);
		}

		public void CloseTooltip()
		{
			InteractManager.Instance.TooltipExit();
		}
	}
}
