using System.Numerics;
using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.Runtime;

class Background : RenderableComponent
{
	public float ScrollSpeed = -100;
	private float skyPosition = 0;

	public override void LoadType()
	{
		// Load the sky background image
		Textures["sky"] = LoadTexture("./assets/image/sky.png");
	}

	public override void Update()
	{
		// Move the background across the screen
		skyPosition += ScrollSpeed * DeltaTime;

		// When the 'first' sky goes fully out of
		// frame then teleport it back to the start
		// so it looks like its in an infinite loop
		if (skyPosition <= -WindowWidth) skyPosition = 0;
	}

	public override void Render2D()
	{
		// Draw both skies
		DrawTexture(Textures["sky"], new Vector2(skyPosition, 0), WindowSize);
		DrawTexture(Textures["sky"], new Vector2(skyPosition + WindowWidth, 0), WindowSize);
	}
}