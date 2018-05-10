using UnityEngine;

namespace Exodrifter.Motive
{
	/// <summary>
	/// A helper for the motive class to keep track of input data.
	/// </summary>
	[AddComponentMenu("")]
	internal class Motive : MonoBehaviour
	{
		#region Data

		/// <summary>
		/// Mouse tracking data.
		/// </summary>
		public static MouseTracker Mouse
		{
			get
			{
				mouse = mouse ?? new MouseTracker(Input.mousePosition);
				return mouse;
			}
		}
		private static MouseTracker mouse;

		#endregion

		#region Monobehaviour

		private void Update()
		{
			Mouse.Update();
		}

		#endregion

		#region Singleton

		private static Motive Instance
		{
			get
			{
				Init();
				return instance;
			}
		}
		private static Motive instance = null;
		private static bool quitting = false;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Init()
		{
			// Check if the tracker has already been initialized
			if (instance != null && !instance.Equals(null))
			{
				return;
			}

			// Don't initialize if the application is quitting
			if (quitting)
			{
				return;
			}

			var gameObject = new GameObject("Motive");
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			DontDestroyOnLoad(gameObject);

			instance = gameObject.AddComponent<Motive>();
		}

		private void Awake()
		{
			// Check if another instance of the tracker already exists
			if (instance != null && !instance.Equals(null))
			{
				if (this != instance)
				{
					Destroy(gameObject);
				}
			}
		}

		private void OnApplicationQuit()
		{
			quitting = true;
		}

		private void OnDestroy()
		{
			if (this == instance)
			{
				instance = null;
			}

			// Re-initialize the tracker
			Init();
		}

		#endregion
	}
}
