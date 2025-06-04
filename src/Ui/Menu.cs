using Smoke;
using static Smoke.Graphics;
using static Smoke.AssetManager;
using Raylib_cs;
using System.Numerics;

class Menu : RenderableComponent
{
	// TODO: do in json
	// TODO: Add support for "mathematical" transforms
	private Transform2D logo = new Transform2D();

	public override void LoadType()
	{
		// Load and apply the font
		Fonts["papyrus"] = LoadFont("./assets/font/papyrus.ttf");
		FontKey = "papyrus";

		// Load the icon
		// TODO: Add this as a smoke feature (no raylib)
		Images["icon"] = LoadImage("./assets/image/icon.png");
		Raylib.SetWindowIcon(Images["icon"]);

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
		GameObject.Get<SplashText>().Position = logo.PositionMax - new Vector2(0, logo.Size.Y);
	}

	public override void Render2D()
	{
		// Draw the logo
		DrawTexture(Textures["logo"], logo, Color.White);
	}
}