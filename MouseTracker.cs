using UnityEngine;

namespace Exodrifter.Motive
{
	/// <summary>
	/// The mouse tracker stores and scrapes data related to Mouse Input from
	/// the Unity API.
	/// </summary>
	public class MouseTracker
	{
		/// <summary>
		/// The position of the mouse this frame.
		/// </summary>
		public Vector3 Position
		{
			get { return position; }
		}
		private Vector3 position;

		/// <summary>
		/// The position of the mouse last frame.
		/// </summary>
		public Vector3 LastPosition
		{
			get { return lastPosition; }
		}
		private Vector3 lastPosition;

		/// <summary>
		/// The change in mouse position since the last frame.
		/// </summary>
		public Vector3 Delta
		{
			get { return delta; }
		}
		private Vector3 delta = Vector3.zero;

		/// The Left (or primary) mouse button.
		/// </summary>
		public KeyTracker Primary
		{
			get { return keys[0]; }
		}

		/// <summary>
		/// Right mouse button (or secondary mouse button).
		/// </summary>
		public KeyTracker Secondary
		{
			get { return keys[1]; }
		}

		/// <summary>
		/// Indexes the buttons on the mouse.
		/// </summary>
		/// <param name="key">The index of the mouse button to get.</param>
		/// <returns>The key tracker for the mouse button.</returns>
		public KeyTracker this[int key]
		{
			get
			{
				return keys[key];
			}
		}

		/// <summary>
		/// The number of buttons on the mouse.
		/// </summary>
		public int ButtonCount
		{
			get { return keys.Length; }
		}

		/// <summary>
		/// An array of buttons on the mouse.
		/// </summary>
		private readonly KeyTracker[] keys = new KeyTracker[]
		{
			new KeyTracker(KeyCode.Mouse0),
			new KeyTracker(KeyCode.Mouse1),
			new KeyTracker(KeyCode.Mouse2),
			new KeyTracker(KeyCode.Mouse3),
			new KeyTracker(KeyCode.Mouse4),
			new KeyTracker(KeyCode.Mouse5),
			new KeyTracker(KeyCode.Mouse6),
		};

		/// <summary>
		/// Creates a new mouse tracker.
		/// </summary>
		internal MouseTracker(Vector3 mousePosition)
		{
			position = mousePosition;
			lastPosition = mousePosition;
		}

		/// <summary>
		/// Updates the state of the tracker.
		/// </summary>
		internal void Update()
		{
			lastPosition = position;
			position = Input.mousePosition;
			delta = position - lastPosition;

			foreach (var key in keys)
			{
				key.Update();
			}
		}
	}
}
