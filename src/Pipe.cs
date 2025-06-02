using System;
using System.Collections.Generic;
using System.Numerics;
using Smoke;
using static Smoke.Runtime;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.SceneManager;
using Raylib_cs;
using Force.DeepCloner;

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

		// Calculate the pipe sizes
		float width = WindowWidth / 6;
		topPipe.Scale = new Vector2(width, WindowHeightHalf - (PipeGapSize + pipeOffset));
		bottomPipe.Scale = new Vector2(width, WindowHeightHalf + (pipeOffset - PipeGapSize));
		pipeGap.Scale = new Vector2(width, PipeGapSize * 2);

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
		if (topPipe.Position.X < (0 - topPipe.Scale.X))
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
		Transform2D player = CurrentScene.Get("Player").Get<Transform2D>();

		// Make collision rectangles for everything
		// TODO: Add collisions to Smoke
		Rectangle playerCollision = new Rectangle(ApplyOrigin(player, Origin.Centre).Position, player.Scale);
		Rectangle topPipeCollision = new Rectangle(ApplyOrigin(topPipe, Origin.BottomCentre).Position, topPipe.Scale);
		Rectangle bottomPipeCollision = new Rectangle(ApplyOrigin(bottomPipe, Origin.TopCentre).Position, bottomPipe.Scale);
		Rectangle gapCollision = new Rectangle(ApplyOrigin(pipeGap, Origin.TopCentre).Position, pipeGap.Scale);

		// TODO: Add collisions to Smoke
		// Check for if we're colliding with a pipe section
		if (
			Raylib.CheckCollisionRecs(playerCollision, topPipeCollision) ||
			Raylib.CheckCollisionRecs(playerCollision, bottomPipeCollision)
		)
		{
			// Kill the player
			Console.WriteLine("dead");
			// debugPlayerColliding = true;
		}

		// Check for if we're passing through the gap
		if (Raylib.CheckCollisionRecs(playerCollision, gapCollision))
		{
			// Update the players score
			GameManager.Score++;

			// Say that we've passed this pipe
			// since its one point per pipe
			BeenPassed = true;
			// debugPlayerColliding = false;
		}
	}

	public override void Render2D()
	{
		// Draw the pipes
		DrawTexture(topPipeSprite.Texture, topPipe, Origin.BottomCentre, Color.White);
		DrawTexture(bottomPipeSprite.Texture, bottomPipe, Origin.TopCentre, Color.White);
	}

	public override void RenderDebug2D()
	{
		Transform2D player = CurrentScene.Get("Player").Get<Transform2D>();
		// 	DrawSquareOutline(ApplyOrigin(player, Origin.Centre), 4f, debugPlayerColliding ? Color.White : Color.Magenta);

		// 	DrawSquareOutline(ApplyOrigin(pipeGap, Origin.TopCentre), 4f, Color.Magenta);
		// 	DrawSquareOutline(ApplyOrigin(bottomPipe, Origin.TopCentre), 4f, Color.Green);


		// Collision collision = GenerateCollision(player, Origin, Pipe, orign);

		Collision collision = new Collision(player, Origin.TopLeft, pipeGap, Origin.TopCentre);
		DrawSquare(collision.GetCollisionTransform(), Color.Beige);

		Rectangle topPipeCollision = new Rectangle(ApplyOrigin(topPipe, Origin.BottomCentre).Position, topPipe.Scale);
		Raylib.DrawRectangleLinesEx(topPipeCollision, 3f, Color.Magenta);

		Rectangle bottomPipeCollision = new Rectangle(ApplyOrigin(bottomPipe, Origin.TopCentre).Position, bottomPipe.Scale);
		Raylib.DrawRectangleLinesEx(bottomPipeCollision, 3f, Color.Magenta);

		Rectangle playerCollision = new Rectangle(ApplyOrigin(player, Origin.Centre).Position, player.Scale);
		Raylib.DrawRectangleLinesEx(playerCollision, 3f, Color.Magenta);
	}


	


	// TODO: Don't do this
	private Transform2D ApplyOrigin(Transform2D transform, Vector2 origin)
	{
		// TODO: Definitely don't do this
		Transform2D newTransform = transform.DeepClone();

		newTransform.Position -= (transform.Scale * origin);
		return newTransform;
	}
}