using MyQueenMySelf.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace MyQueenMySelf.UI
{
	public class SliderVolume : MonoBehaviour
	{
        void Start()
        {
			Slider slider = GetComponent<Slider>();
			slider.value = MusicManager.Instance.GetVolume();
        }

        public void UpdateVolume(float volume)
		{
			MusicManager.Instance.UpdateVolume(volume);
		}


	}
}
