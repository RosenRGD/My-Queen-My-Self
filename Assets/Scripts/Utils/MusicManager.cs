using UnityEngine;

namespace MyQueenMySelf.Utils
{
	public class MusicManager : MonoBehaviour
	{
		private static MusicManager instance;

		public static MusicManager Instance
		{
			get
			{
				if (_isShuttingDown) return null;

				if (instance == null)
				{
					instance = FindFirstObjectByType<MusicManager>();

					if (instance == null)
					{
						GameObject singleton = new GameObject(typeof(MusicManager).ToString());
						instance = singleton.AddComponent<MusicManager>();
						DontDestroyOnLoad(singleton);
					}
				}
				return instance;
			}
		}

		private static bool _isShuttingDown = false;

		[SerializeField] AudioClip menuTrack;
		[SerializeField] AudioClip worldTrack;
		[SerializeField] AudioClip dreamTrack;
		[SerializeField] AudioClip winTrack;
		[SerializeField] AudioClip loseTrack;


		AudioSource _audioSource;

		void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else if (instance != this)
			{
				Destroy(gameObject);
			}

			_audioSource = GetComponent<AudioSource>();
			if (_audioSource == null)
			{
				_audioSource = gameObject.AddComponent<AudioSource>();
			}

			_audioSource.loop = true;
			_audioSource.playOnAwake = false;
			PlayMenuTrack();
		}

		public void UpdateVolume(float volume)
		{
			_audioSource.volume = volume;
		}

		void OnApplicationQuit()
		{
			_isShuttingDown = true;
		}

		public void PlayMenuTrack()
		{
			_audioSource.clip = menuTrack;
			_audioSource.Play();
		}

		public void PlayWorldTrack()
		{
			_audioSource.clip = worldTrack;
			_audioSource.Play();
		}

		public void PlayDreamTrack()
		{
			_audioSource.clip = dreamTrack;
			_audioSource.Play();
		}

		public void PlayWinTrack()
		{
			_audioSource.clip = winTrack;
			_audioSource.Play();
		}

		public void PlayLoseTrack()
		{
			_audioSource.clip = loseTrack;
			_audioSource.Play();
		}

		public void StopMusic()
		{
			_audioSource.Stop();
		}

		public float GetVolume()
		{
			return _audioSource.volume;
		}
	}
}
