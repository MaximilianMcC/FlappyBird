using System;
using Smoke;
using static Smoke.SceneManager;

class PipeSpawner : UpdatableComponent
{
	private Timer timer => GameObject.Get<Timer>();

	public override void Update()
	{
		// Check for if we can spawn a pipe
		if (GameManager.Paused) return;
		if (timer.Done == false) return;

		// Spawn a pipe
		CurrentScene.CreatePrefab("pipe", "pipe");
	}
}