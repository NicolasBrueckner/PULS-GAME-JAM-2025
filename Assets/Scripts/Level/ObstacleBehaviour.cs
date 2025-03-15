using UnityEngine;
using static Kaputt;
using static IDamageable;

public class ObstacleBehaviour : MonoBehaviour, IDamageable
{
	public bool hasHealthUp = false;

	void Start()
	{
		SetActiveByName("Obstacle", true);
		SetActiveByName("Loot", false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			GameplayEventManager.Instance?.OnPlayerHit();
		}     
	}

    public void DestroyObject()
    {
        // TODO: Animation für Übergang oder so?
        SetActiveByName("Obstacle", false);
		if (hasHealthUp)
			GameplayEventManager.Instance?.OnPlayerHeal();
    }
}