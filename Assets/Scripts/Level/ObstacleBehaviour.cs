using UnityEngine;
using static Kaputt;

public class ObstacleBehaviour : MonoBehaviour
{
	public bool hasHealthUp = false;
	// public float damageOnCollision = 1.0f;

	void Start()
	{
		SetActiveByName("Obstacle", true);
		SetActiveByName("Loot", false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			// TODO: irgendwie hit type unterscheiden
			GameplayEventManager.Instance?.OnPlayerHit();
			OnDestroyed();
		}     
	}

	private void OnDestroyed()
	{
		// TODO: Animation f�r �bergang oder so?
		SetActiveByName("Obstacle", false);
		if (hasHealthUp)
			SetActiveByName("Loot", true);
	}
}