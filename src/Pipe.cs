using System;
using System.Collections.Generic;
using System.Numerics;
using Smoke;
using static Smoke.Runtime;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using Raylib_cs;

class Pipe : RenderableComponent
{
	public float Speed = 100f;

	// TODO: Don't do this tuple garbage
	private int pipeTextureIndex = 0;
	private float xPosition;
	private float gap;

	// Load all the pipe textures
	public override void LoadType()
	{
		// Textures["pipe1A"] = LoadTexture("./assets/image/pipes/pipe1a.png");
		// Textures["pipe1A"] = LoadTexture("./assets/image/pipes/pipe1b.png");

		// Textures["pipe2A"] = LoadTexture("./assets/image/pipes/pipe2a.png");
		// Textures["pipe2B"] = LoadTexture("./assets/image/pipes/pipe2b.png");

		// Textures["pipe3A"] = LoadTexture("./assets/image/pipes/pipe3a.png");
		// Textures["pipe3B"] = LoadTexture("./assets/image/pipes/pipe3b.png");
	}

	public override void Start()
	{
		Console.WriteLine("kia ora");
		Random random = new Random();

		// Pick a texture set to use
		// TODO: Don't do like this
		pipeTextureIndex = random.Next(1, 3);

		// See how tall the inside
		// section of the pipe will be
		// (where player goes)
		//? Divided by 2 because we calculate from 0.5 on the y
		// gap = random.Next(100, 200);
		gap = 200;
		gap /= 2;

		// Spawn them somewhere offscreen
		// in front of the player idk
		// TODO: Actually measure this value
		xPosition = WindowWidth + 100;
	}

	public override void Update()
	{
		// Move the pipes
		xPosition -= Speed * DeltaTime;

		// Check for if it goes off screen and despawn
		// if (xPosition + )
	}

	public override void Render2D()
	{
		// Calculate the sizes of the pipes
		// TODO: Don't do in render
		Vector2 size = new Vector2(
			WindowWidth / 6,
			(WindowHeight / 2) - gap
		);

		// Calculate the positions of the pipes
		Vector2 topPosition = new Vector2(xPosition, (WindowHeight / 2) - gap);
		Vector2 bottomPosition = new Vector2(xPosition, (WindowHeight / 2) + gap);

		// Draw the pipes
		DrawTexture(Textures[$"pipe{pipeTextureIndex}A"], topPosition, size, Origin.BottomCentre, 0f, Color.White);
		DrawTexture(Textures[$"pipe{pipeTextureIndex}B"], bottomPosition, size, Origin.TopCentre, 0f, Color.White);
	}

	public override void RenderDebug2D()
	{
		DrawCircle(WindowSize / 2, 10f, Color.Magenta);
	}
}