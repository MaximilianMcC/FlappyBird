using System;
using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;

class Player : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();
	public const float Gravity = 1000f;
	public const float JumpForce = 650f;
	public const float RotationSpeed = 50f;

	private float acceleration;
	private float rotation;

	public override void LoadType()
	{
		// Load the player
		Textures["bird"] = LoadTexture("./assets/image/bird.png");
	}

	public override void Update()
	{
		// Move the bird down (gravity)
		acceleration += Gravity * DeltaTime;

		// Check for if we wanna jump (click or space or up arrow)
		if (MouseClicked(Raylib_cs.MouseButton.Left) || KeyPressed(Raylib_cs.KeyboardKey.Space) || KeyPressed(Raylib_cs.KeyboardKey.Up))
		{
			// Move the bird up (jumping)
			//? No delta time because it only happens once
			acceleration = -JumpForce;

			// Angle the bird up
			// rotation += -RotationSpeed;
			rotation = -25;
			rotation %= 360;
		}

		// Update the players position
		Transform.Position.Y += acceleration * DeltaTime;

		// As we fall then rotate the player to
		// make it look like they're falling idk
		if (acceleration > 0)
		{
			// Angle the bird downwards if its on
			// less than a 45 degree angle
			if (rotation < 45)
			{
				rotation += RotationSpeed * DeltaTime;
				rotation %= 360;
			}
		}
	}

	public override void Render2D()
	{
		DrawTexture(Textures["bird"], Transform, rotation, Raylib_cs.Color.White);
	}
}