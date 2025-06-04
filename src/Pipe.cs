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
	public bool BeenPassed;
	private float pipeOffset;

	private List<string> topPipeTextureKeys;
	private List<string> bottomPipeTextureKeys;
	private Sprite topPipeSprite => GameObject.Get<Sprite>(0);
	private Sprite bottomPipeSprite => GameObject.Get<Sprite>(1);

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
		int topTextureIndex = random.Next(0, topPipeTextureKeys.Count);
		int bottomTextureIndex = random.Next(0, bottomPipeTextureKeys.Count);

		// Assign the textures to the sprites
		topPipeSprite.SetFrames(topPipeTextureKeys[topTextureIndex]);
		bottomPipeSprite.SetFrames(bottomPipeTextureKeys[bottomTextureIndex]);



		// Get a random offset (pipe y position)
		//? Divided by 2 because we calculate from 0.5 on the y
		pipeOffset = random.Next(-PipeOffsetRange, PipeOffsetRange);
		pipeOffset /= 2;

		// Give the pipes origins
		topPipe.Origin = Origin.BottomCentre;
		bottomPipe.Origin = Origin.TopCentre;
		pipeGap.Origin = Origin.TopCentre;

		// Calculate the pipe sizes
		float width = WindowWidth / 6;
		topPipe.Size = new Vector2(width, WindowHeightHalf - (PipeGapSize + pipeOffset));
		bottomPipe.Size = new Vector2(width, WindowHeightHalf + (pipeOffset - PipeGapSize));

		// Set the gap to have a tiny width so the
		// player gets the score as they pass through
		// the pipe, not the second it appears
		pipeGap.Size = new Vector2(10, PipeGapSize * 2);

		// Set the pipes to spawn offscreen
		// TODO: Make it so they can have a parent transform
		float x = WindowWidth + width;
		topPipe.Position = new Vector2(x, (WindowHeightHalf - PipeGapSize) - pipeOffset);
		bottomPipe.Position = new Vector2(x, (WindowHeightHalf + PipeGapSize) - pipeOffset);
		pipeGap.Position = topPipe.Position;
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
		if (topPipe.Position.X < (0 - topPipe.Size.X))
		{
			GameObject.RemoveFromScene();
			return;
		}

		// Check for player collision
		if (BeenPassed == false) CheckPlayerCollision();
	}

	private void CheckPlayerCollision()
	{
		// Get the players transform
		// TODO: Maybe make this like private (don't call all the time)
		Player player = CurrentScene.Get("Player").Get<Player>();

		// Check for pipe collision
		if (player.Transform.Overlaps(topPipe) || player.Transform.Overlaps(bottomPipe))
		{
			// Say the player is dead
			player.Dead = true;
			return;
		}

		// Check for gap collision
		if (BeenPassed == false && player.Transform.Overlaps(pipeGap))
		{
			// Say that we've passed the pipe
			BeenPassed = true;

			// Update the players score
			GameManager.Score++;

			// TODO: Play a sound effect or something
		}
	}

	public override void Render2D()
	{
		DrawSprite(topPipeSprite, topPipe, Color.White);
		DrawSprite(bottomPipeSprite, bottomPipe, Color.White);
	}
}