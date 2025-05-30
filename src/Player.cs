using System;
using System.Numerics;
using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;

class Player : RenderableComponent
{
	public Transform2D Transform => GameObject.Get<Transform2D>();
	public const float Gravity = 1500f;
	public const float JumpForce = 500f;
	private float acceleration;

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
		}

		// Update the players position
		Transform.Position.Y += acceleration * DeltaTime;
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 10f, Raylib_cs.Color.Lime);
		DrawTexture(Textures["bird"], Transform, Origin.Centre, Raylib_cs.Color.White);

		DrawText($"{acceleration}", Vector2.Zero, 75f, Raylib_cs.Color.Black);
	}
}