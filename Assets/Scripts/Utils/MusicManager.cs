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


		private AudioSource _audioSource;

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
        }

        void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }

	}
}
