using System.Numerics;
using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.Runtime;

class Background : RenderableComponent
{
	public float ScrollSpeed = -50;
	private float skyPosition = 0;
	private float groundPosition = 0;

	// TODO: Jst use a transform
	private Vector2 groundSize;

	public override void LoadType()
	{
		// Load the background images
		Textures["sky"] = LoadTexture("./assets/image/sky.png");
		Textures["ground"] = LoadTexture("./assets/image/ground.png");

		groundSize = new Vector2(WindowWidth, WindowHeight / 4);
	}

	public override void Update()
	{
		// Move the backgrounds across the screen
		skyPosition += ScrollSpeed * DeltaTime;
		groundPosition += (ScrollSpeed * 2f) * DeltaTime;

		// When the 'first' one goes fully out of
		// frame then teleport it back to the start
		// so it looks like its in an infinite loop
		if (skyPosition <= -WindowWidth) skyPosition = 0;
		if (groundPosition <= -WindowWidth) groundPosition = 0;
	}

	public override void Render2D()
	{
		// Draw both skies
		DrawTexture(Textures["sky"], new Vector2(skyPosition, 0), WindowSize);
		DrawTexture(Textures["sky"], new Vector2(skyPosition + WindowWidth, 0), WindowSize);

		// Draw both grounds
		DrawTexture(Textures["ground"], new Vector2(groundPosition, WindowHeight - groundSize.Y), groundSize);
		DrawTexture(Textures["ground"], new Vector2(groundPosition + WindowWidth, WindowHeight - groundSize.Y), groundSize);
	}
}