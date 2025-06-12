using System;
using Raylib_cs;
using System.Numerics;
using Smoke;
using static Smoke.Graphics;

class SplashText : RenderableComponent
{
	public Vector2 Position;
	private float currentFontSize;

	public float FontSize;
	public string[] Texts;
	private int textsIndex;

	public override void Start()
	{
		// Pick a random splash text to use
		Random random = new Random();
		textsIndex = random.Next(0, Texts.Length);
	}

	public override void Update()
	{
		// Calculate how big the font size should be
		float time = (float)Raylib.GetTime();
		float pulse = 1f + 0.05f * MathF.Sin(time * 8);
		currentFontSize = FontSize * pulse;
	}

	public override void Render2D()
	{
		DrawText(Texts[textsIndex], Position, Origin.Centre, 15f, currentFontSize, Color.Yellow);
	}
}