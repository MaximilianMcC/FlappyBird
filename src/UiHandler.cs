using System;
using System.Numerics;
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
	}

	public override void Update()
	{
		// Check for if we wanna pause
		// TODO: Make it pause on any key thats not a jump one
		// TODO: Make it pause if focus is lost
		if (KeyPressed(Raylib_cs.KeyboardKey.Escape)) GameManager.Paused = !GameManager.Paused;
	}

	public override void Render2D()
	{
		// Say if the games paused
		// TODO: Put stupid texture here
		if (GameManager.Paused) DrawTextCentered("paused", WindowSize, Vector2.One, 75f, Raylib_cs.Color.Black);
	}
}