using System;
using UnityEngine;

public class KeyTracker
{
	/// <summary>
	/// An event for when the key is pressed down.
	/// </summary>
	public event Action OnDown;

	/// <summary>
	/// An event for when the key is released.
	/// </summary>
	public event Action OnUp;

	/// <summary>
	/// True if this key is held down this frame, false otherwise.
	/// </summary>
	public bool IsDown
	{
		get { return isDown; }
	}
	private bool isDown = false;

	/// <summary>
	/// True if this key was held down last frame, false otherwise.
	/// </summary>
	public bool WasDown
	{
		get { return wasDown; }
	}
	private bool wasDown = false;

	/// <summary>
	/// True if this key was pressed down this frame.
	/// </summary>
	public bool JustPressed
	{
		get { return isDown && !wasDown; }
	}

	/// <summary>
	/// True if this key was released down this frame.
	/// </summary>
	public bool JustReleased
	{
		get { return !isDown && wasDown; }
	}

	/// <summary>
	/// The key code to track.
	/// </summary>
	private readonly KeyCode code;

	/// <summary>
	/// The amount of time this key has been in this state.
	/// </summary>
	private float time = 0;

	/// <summary>
	/// Creates a new tracker for the specified key.
	/// </summary>
	/// <param name="code">The key code to track.</param>
	public KeyTracker(KeyCode code)
	{
		this.code = code;
	}

	internal virtual void Update()
	{
		wasDown = isDown;
		isDown = Input.GetKey(code);

		if (isDown != wasDown)
		{
			time = Time.deltaTime;

			if (isDown)
			{
				if (OnDown != null)
				{
					OnDown();
				}
			}
			else
			{
				if (OnUp != null)
				{
					OnUp();
				}
			}
		}
		else
		{
			time += Time.deltaTime;
		}
	}
}
