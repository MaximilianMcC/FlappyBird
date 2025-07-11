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
	private Sprite sprite => GameObject.Get<Sprite>();

	public const float Gravity = 1300f;
	public const float JumpForce = 400f;
	private float acceleration;

	public override void LoadType()
	{
		// Load the player
		Textures["bird"] = LoadTexture("./assets/image/bird.png");
	}

	public override void Start()
	{
		sprite.SetFrames("bird");
		Transform.Position.X = WindowWidthHalf;
	}

	public override void Update()
	{
		if (GameManager.Paused) return;

		// Move the bird down (gravity)
		acceleration += Gravity * DeltaTime;

		// Check for if we wanna jump (click or space or up arrow)
		if (GameManager.GameOver == false && (
			MouseClicked(Raylib_cs.MouseButton.Left) ||
			KeyPressed(Raylib_cs.KeyboardKey.Space) ||
			KeyPressed(Raylib_cs.KeyboardKey.Up)
		))
		{
			// Move the bird up (jumping)
			//? No delta time because it only happens once
			acceleration = -JumpForce;
		}

		// Update the players position
		Transform.Position.Y += acceleration * DeltaTime;

		// If the player goes offscreen then kill them
		// TODO: Account for origin
		if (Transform.Position.Y > WindowHeight) GameManager.GameOver = true;
		if (Transform.Position.Y < 0) GameManager.GameOver = true;
	}

	public override void Render2D()
	{
		DrawSprite(sprite, Transform, Raylib_cs.Color.White);
	}
}