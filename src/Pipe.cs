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

	private int pipeTextureIndex = 0;
	private float pipeOffset;

	// TODO: Use vectors
	private float xPosition;
	private float xWidth;

	//? Is actually 200 because its drawn on top/bottom (twice)
	public float pipeGap = 100;
	// TODO: Don't use int (cast)
	public int PipeOffset = 200;

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
		// Comment addiction
		Random random = new Random();

		// Pick a texture set to use
		// TODO: Don't do like this
		pipeTextureIndex = random.Next(1, 3);

		// Get a random offset (pipe y position)
		//? Divided by 2 because we calculate from 0.5 on the y
		pipeOffset = random.Next(-PipeOffset, PipeOffset);
		pipeOffset /= 2;

		// Spawn them somewhere offscreen
		// in front of the player idk
		// TODO: Actually measure this value
		xPosition = WindowWidth + 100;
		xWidth = WindowWidth / 6;
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
		// TODO: Pre calculate/bake
		float topSizeY = (WindowHeight / 2) - (pipeGap + pipeOffset);
		float bottomSizeY = (WindowHeight / 2) + (pipeOffset - pipeGap);

		// Calculate the positions of the pipes
		// TODO: Pre calculate/bake
		float topY = ((WindowHeight / 2) - pipeGap) - pipeOffset;
		float bottomY = ((WindowHeight / 2) + pipeGap) - pipeOffset;

		// Draw the pipes
		DrawTexture(Textures[$"pipe{pipeTextureIndex}A"], new Vector2(xPosition, topY), new Vector2(xWidth, topSizeY), Origin.BottomCentre, 0f, Color.White);
		DrawTexture(Textures[$"pipe{pipeTextureIndex}B"], new Vector2(xPosition, bottomY), new Vector2(xWidth, bottomSizeY), Origin.TopCentre, 0f, Color.White);
	}

	public override void RenderDebug2D()
	{
		DrawCircle(WindowSize / 2, 10f, Color.Magenta);
	}
}