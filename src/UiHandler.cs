using Smoke;
using static Smoke.AssetManager;
using static Smoke.Graphics;

class UiHandler : Component
{
	public override void LoadType()
	{
		// Load and apply the font
		Fonts["papyrus"] = LoadFont("./assets/font/papyrus.ttf");
		FontKey = "papyrus";
	}
}