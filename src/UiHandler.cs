using System;
using System.Numerics;
using Raylib_cs;
using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;
using static Smoke.Input;

class UiHandler : RenderableComponent
{
	public override void LoadType()
	{
		// Load and apply the font
		Fonts["papyrus"] = LoadFont("./assets/font/papyrus.ttf");
		FontKey = "papyrus";

		// Load the icon
		// TODO: Add this as a smoke feature
		Images["icon"] = LoadImage("./assets/image/icon.png");
		Raylib.SetWindowIcon(Images["icon"]);
	}

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
		if (GameManager.Paused) DrawTextCentered("paused", WindowSize, Vector2.One, 75f, Color.Black);

		// Show the score
		// TODO: Make it bounce around or something idk
		DrawText($"score: {GameManager.Score}", Vector2.One * 10, 75f, Color.Black);
	}

	public override void RenderDebug2D()
	{
		DrawText($"fps: {Raylib.GetFPS()}", new Vector2(10, 100), 100f, Color.Black);
	}
}