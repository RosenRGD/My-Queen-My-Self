using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyQueenMySelf.UI
{
    public class TooltipManager : MonoBehaviour
    {
        TextMeshProUGUI _text;
        Image[] _images;

        void Awake()
        {
            _images = GetComponentsInChildren<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
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
