using System;
using Exodrifter.Motive;
using UnityEngine;

public class MouseKeyTracker : KeyTracker
{
	/// <summary>
	/// True if this key was just clicked this frame.
	/// </summary>
	public bool JustClicked
	{
		get { return justClicked; }
	}
	private bool justClicked;

	/// <summary>
	/// True if this key was just started dragging this frame.
	/// </summary>
	public bool JustStartedDrag
	{
		get { return justStartedDrag; }
	}
	private bool justStartedDrag;

	/// <summary>
	/// True if this key was just ended dragging this frame.
	/// </summary>
	public bool JustEndedDrag
	{
		get { return justEndedDrag; }
	}
	private bool justEndedDrag;

	/// <summary>
	/// An event for when the key is pressed and released without the mouse
	/// moving.
	/// </summary>
	public event Action OnClick;

	/// <summary>
	/// An event for when the key is pressed and the mouse starts moving.
	/// </summary>
	public event Action<Vector3> OnDragStart;

	/// <summary>
	/// An event for when the key is released after the drag event starts.
	/// </summary>
	public event Action<Vector3> OnDragEnd;

	/// <summary>
	/// The tracker to use for drag events.
	/// </summary>
	private MouseTracker mouse;

	private Vector3 dragStartPosition;
	private Vector3 dragDelta;
	private bool dragStarted;

	/// <summary>
	/// Creates a new mouse key tracker.
	/// </summary>
	/// <param name="code">The key code to track.</param>
	/// <param name="tracker">The tracker to use for drag events.</param>
	public MouseKeyTracker(KeyCode code, MouseTracker mouse)
		: base(code)
	{
		this.mouse = mouse;
	}

	internal override void Update()
	{
		base.Update();
		justClicked = false;
		justStartedDrag = false;
		justEndedDrag = false;

		if (JustPressed)
		{
			dragStartPosition = mouse.Position;
			dragDelta = Vector3.zero;
		}
		else if (JustReleased)
		{
			if (dragStarted)
			{
				dragStarted = false;

				justEndedDrag = true;
				if (OnDragEnd != null)
				{
					OnDragEnd(dragStartPosition);
				}
			}
			else
			{
				justClicked = true;

				if (OnClick != null)
				{
					OnClick();
				}
			}
		}
		else if (IsDown)
		{
			if (!dragStarted)
			{
				dragDelta += mouse.Delta;

				if (dragDelta.sqrMagnitude >= mouse.SqrDragDistance)
				{
					justStartedDrag = true;
					if (OnDragStart != null)
					{
						OnDragStart(dragStartPosition);
					}

					dragStarted = true;
				}
			}
		}
		else
		{
			dragStarted = false;
		}
	}
}
