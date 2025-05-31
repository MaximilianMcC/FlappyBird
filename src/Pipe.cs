using System;
using System.Collections.Generic;
using System.Numerics;
using Smoke;
using static Smoke.Runtime;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.SceneManager;
using Raylib_cs;

class Pipe : RenderableComponent
{
	public float Speed = 100f;
	private float pipeOffset;

	private List<string> topPipeTextureKeys;
	private List<string> bottomPipeTextureKeys;
	private int topPipeTextureIndex;
	private int bottomPipeTextureIndex;

	// TODO: do NOT do this
	private Transform2D topPipe = new();
	private Transform2D pipeGap = new();
	private Transform2D bottomPipe = new();

	public float PipeGapSize = 100;
	public int PipeOffsetRange = 100;

	// Load all the pipe textures
	public override void LoadType()
	{
		Textures["pipe1A"] = LoadTexture("./assets/image/pipe/pipe1a.png");
		Textures["pipe2A"] = LoadTexture("./assets/image/pipe/pipe2a.png");
		topPipeTextureKeys = ["pipe1A", "pipe2A"];

		Textures["pipe1B"] = LoadTexture("./assets/image/pipe/pipe1b.png");
		Textures["pipe2B"] = LoadTexture("./assets/image/pipe/pipe2b.png");
		bottomPipeTextureKeys = ["pipe1B", "pipe2B"];
	}

	public override void Start()
	{
		// Comment addiction
		Random random = new Random();

		// Pick a texture set to use
		// TODO: Don't do like this
		topPipeTextureIndex = random.Next(0, topPipeTextureKeys.Count);
		bottomPipeTextureIndex = random.Next(0, bottomPipeTextureKeys.Count);

		// Get a random offset (pipe y position)
		//? Divided by 2 because we calculate from 0.5 on the y
		pipeOffset = random.Next(-PipeOffsetRange, PipeOffsetRange);
		pipeOffset /= 2;

		// Calculate the pipe sizes
		float width = WindowWidth / 6;
		topPipe.Scale = new Vector2(width, WindowHeightHalf - (PipeGapSize + pipeOffset));
		bottomPipe.Scale = new Vector2(width, WindowHeightHalf + (pipeOffset - PipeGapSize));
		pipeGap.Scale = new Vector2(width, PipeGapSize);

		// Set the pipes to spawn offscreen
		// TODO: Make it so they can have a parent transform
		float x = WindowWidth + width;
		topPipe.Position = new Vector2(x, (WindowHeightHalf - PipeGapSize) - pipeOffset);
		bottomPipe.Position = new Vector2(x, (WindowHeightHalf + PipeGapSize) - pipeOffset);
		pipeGap.Position = new Vector2(x, WindowHeight - PipeGapSize);
	}

	public override void Update()
	{
		if (GameManager.Paused) return;

		// Move the pipes
		float movement = Speed * DeltaTime;
		topPipe.Position.X -= movement;
		pipeGap.Position.X -= movement;
		bottomPipe.Position.X -= movement;

		// Check for if it goes off screen and despawn
		if (topPipe.Position.X < (0 - topPipe.Scale.X))
		{
			GameObject.RemoveFromScene();
			return;
		}

		// Check for player collision
		CheckPlayerCollision();
	}

	private void CheckPlayerCollision()
	{
		// Get the players transform
		// Transform2D player = CurrentScene.Get("Player").Get<Transform2D>();

		// Check for if they're in 'range' of a pipe
		// TODO: Use rectangles and raylib collision methods
		// TODO: Add a collision component to smoke
		// if (player.Position.X >= xPosition)
	}

	public override void Render2D()
	{
		// Draw the pipes
		DrawTexture(Textures[topPipeTextureKeys[topPipeTextureIndex]], topPipe, Origin.BottomCentre, Color.White);
		DrawTexture(Textures[bottomPipeTextureKeys[bottomPipeTextureIndex]], bottomPipe, Origin.TopCentre, Color.White);
	}
}