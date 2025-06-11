using System;
using System.Numerics;
using Raylib_cs;
using Smoke;
using static Smoke.Graphics;
using static Smoke.Input;

class Button : RenderableComponent
{
	public string Text;
	public float FontSize;
	private Transform2D button => GameObject.Get<Transform2D>();
	private bool hovered;

	public override void Start()
	{
		// Dynamically size the button
		button.Origin = Origin.Centre;
		button.Position = WindowSize * Origin.Centre;
		button.Size = WindowWidth * new Vector2(0.5f, 0.2f);
	}

	public override void Update()
	{
		// Check for if we're hovering over the button
		if (button.Contains(MousePosition()))
		{
			// Toggle the cursor if needed
			if (hovered == false)
			{
				hovered = true;
				Raylib.SetMouseCursor(MouseCursor.PointingHand);
			}
		}
		else
		{
			// Toggle the cursor if needed
			if (hovered == false) return;
			hovered = false;
			Raylib.SetMouseCursor(MouseCursor.Default);
			return;
		}

		// Check for if we clicked ont he button
		if (MouseClicked(MouseButton.Left))
		{
			// Run the button idk
			// TODO: Run a method or update a public bool
			Raylib.SetMouseCursor(MouseCursor.Default);
			SceneManager.SetScene("game");
		}

	}

	public override void Render2D()
	{
		// Draw the buttons background
		DrawSquare(button, Color.Blue);

		// Draw the text
		Transform2D textTransform = new Transform2D(button);
		DrawText(Text, textTransform, FontSize, Color.Black);
	}
}