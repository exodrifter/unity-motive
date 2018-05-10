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
		}
	}
}
