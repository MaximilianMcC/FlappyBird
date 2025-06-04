using System;
using System.Numerics;
using Raylib_cs;
using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.Input;

class UiHandler : RenderableComponent
{
	public override void Update()
	{
		// Check for if we wanna pause
		// TODO: Make it pause on any key thats not a jump one
		// TODO: Make it pause if focus is lost
		if (KeyPressed(KeyboardKey.Escape)) GameManager.Paused = !GameManager.Paused;
	}

	public override void Render2D()
	{
		// Say if the games paused
		// TODO: Put stupid texture here
		if (GameManager.Paused)
		{
			DrawTextCentered("paused", WindowSize, Vector2.One, 75f, Color.Black);
			return;
		}

		// Depending on if we're dead
		if (GameManager.GameOver)
		{
			DrawTextCentered("game over", WindowSize, Vector2.One, 100f, Color.Black);
			DrawTextCentered($"\n\nscore: {GameManager.Score}", WindowSize, Vector2.One, 80f, Color.Black);
		}
		else
		{
			// Show the score
			DrawText($"score: {GameManager.Score}", Vector2.One * 10, Origin.TopLeft, 0f, 75f, Color.Black);
		}
	}

	public override void RenderDebug2D()
	{
		DrawText($"fps: {Raylib.GetFPS()}", new Vector2(10, 100), Origin.TopLeft, 0f, 100f, Color.Black);
	}
}