using Smoke;
using static Smoke.Graphics;
using static Smoke.AssetManager;
using Raylib_cs;
using System.Numerics;

class Menu : RenderableComponent
{
	public override void LoadType()
	{
		// Load and apply the font
		Fonts["papyrus"] = LoadFont("./assets/font/papyrus.ttf");
		FontKey = "papyrus";

		// Load the icon
		// TODO: Add this as a smoke feature (no raylib)
		Images["icon"] = LoadImage("./assets/image/icon.png");
		Raylib.SetWindowIcon(Images["icon"]);
	}

	public override void Render2D()
	{
		DrawTextCentered("manky bird", new Vector2(WindowWidth, WindowHeightHalf), new Vector2(1, 1f), 150, Color.Black);

		string splashText = "hey repco!";
		DrawText(splashText, Vector2.Zero, 25f, 50f, Color.Yellow);
	}
}