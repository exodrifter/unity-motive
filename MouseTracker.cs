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
		public MouseKeyTracker Primary
		{
			get { return keys[0]; }
		}

		/// <summary>
		/// Right mouse button (or secondary mouse button).
		/// </summary>
		public MouseKeyTracker Secondary
		{
			get { return keys[1]; }
		}

		/// <summary>
		/// Indexes the buttons on the mouse.
		/// </summary>
		/// <param name="key">The index of the mouse button to get.</param>
		/// <returns>The key tracker for the mouse button.</returns>
		public MouseKeyTracker this[int key]
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
		/// The squared distance in pixels before a mouse movement accompanied
		/// by a button press is considered a drag.
		/// </summary>
		public float SqrDragDistance
		{
			get { return 100; }
		}

		/// <summary>
		/// An array of buttons on the mouse.
		/// </summary>
		private readonly MouseKeyTracker[] keys;

		/// <summary>
		/// Creates a new mouse tracker.
		/// </summary>
		internal MouseTracker(Vector3 mousePosition)
		{
			position = mousePosition;
			lastPosition = mousePosition;

			keys = new MouseKeyTracker[]
			{
				new MouseKeyTracker(KeyCode.Mouse0, this),
				new MouseKeyTracker(KeyCode.Mouse1, this),
				new MouseKeyTracker(KeyCode.Mouse2, this),
				new MouseKeyTracker(KeyCode.Mouse3, this),
				new MouseKeyTracker(KeyCode.Mouse4, this),
				new MouseKeyTracker(KeyCode.Mouse5, this),
				new MouseKeyTracker(KeyCode.Mouse6, this),
			};
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

		public bool Raycast<T>(out T component)
		{
			if (Camera.main != null && !Camera.main.Equals(null))
			{
				var hit = new RaycastHit();
				var ray = Camera.main.ScreenPointToRay(position);
				if (Physics.Raycast(ray, out hit))
				{
					component = hit.collider.GetComponent<T>();
					if (component != null && !component.Equals(null))
					{
						return true;
					}
				}
			}

			component = default(T);
			return false;
		}
	}
}
