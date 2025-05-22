using MyQueenMySelf.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyQueenMySelf.UI
{
    public class TooltipManager : MonoBehaviour
    {
        public TextMeshProUGUI _text;
        Image[] _images;

        void Awake()
        {
            _images = GetComponentsInChildren<Image>(true);
            _text = GetComponentInChildren<TextMeshProUGUI>(true);
            TurnOffTooltip();   
        }

        void OnEnable()
        {
            InteractManager.Instance.OnTooltipEnterEvent += TurnOnTooltip;
            InteractManager.Instance.OnTooltipExitEvent += TurnOffTooltip;
        }

        void OnDisable()
        {
            InteractManager.Instance.OnTooltipEnterEvent -= TurnOnTooltip;
            InteractManager.Instance.OnTooltipExitEvent -= TurnOffTooltip;
        }

        public void TurnOnTooltip(string tooltipText)
        {
            foreach (Image image in _images)
            {
                image.gameObject.SetActive(true);
            }
            _text.text = tooltipText;
        }

        public void TurnOffTooltip()
        {
            foreach (Image image in _images)
            {
                image.gameObject.SetActive(false);
            }
        }
    }
}
