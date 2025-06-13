using Smoke;
using static Smoke.Graphics;
using static Smoke.AssetManager;
using static Smoke.SceneManager;
using Raylib_cs;
using System.Numerics;
using System;

class Menu : RenderableComponent
{
	// TODO: do in json
	// TODO: Add support for "mathematical" transforms
	private Transform2D logo => GameObject.Get<Transform2D>();
	private Button playButton => CurrentScene.Get("PlayButton").Get<Button>();

	public override void LoadType()
	{
		// Load the icon
		SetIcon("./assets/image/icon.png");

		// Load and apply the font
		Fonts["papyrus"] = LoadFont("./assets/font/papyrus.ttf");
		FontKey = "papyrus";

		// Load the menu logo thing
		Textures["logo"] = LoadTexture("./assets/image/logo.png");

		
	}

	public override void Start()
	{
		// Position the logo
		logo.Position = WindowSize * new Vector2(0.5f, 0.15f);
		logo.Size = new Vector2(3, 1) * (WindowWidth / 5);
		logo.Origin = Origin.Centre;

		// Set the splash text to be on
		// the corner of the menu image
		GameObject.Get<SplashText>().Position = logo.BottomCorner - new Vector2(0, logo.Size.Y);
	}

	public override void Update()
	{
		if (playButton.Clicked) GameManager.StartGame();
	}

	public override void Render2D()
	{
		// Draw the logo
		DrawTexture(Textures["logo"], logo, Color.White);

		DrawText($"v{Project.Version.Major}.{Project.Version.Minor}", logo.BottomCorner + Vector2.UnitY * 20, Origin.BottomRight, 0f, 50f, Color.SkyBlue);
	}
}