using Smoke;
using static Smoke.SceneManager;

static class GameManager
{
	public static int Score;
	public static bool Paused;
	public static bool GameOver;

	public static void StartGame()
	{
		// Reset the score
		GameOver = false;
		Score = 0;

		// Reset the scene
		LoadScene("game");
		CurrentScene.Get("RestartButton").Get<Button>().Enabled = false;
	}
}