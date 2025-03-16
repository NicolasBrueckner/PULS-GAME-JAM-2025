#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class AbilityBehavior : MonoBehaviour
{
	public GameObject shockWaveGameObject;
	public GameObject bashObject;
	public GameObject dashObject;

    public AudioClip abilityLostSound;
    public AudioSource audioSource;

    private List<IAbility> _abilities;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_abilities = new()
		{
			shockWaveGameObject.GetComponent<IAbility>(),
			bashObject.GetComponent<IAbility>(),
			dashObject.GetComponent<IAbility>(),
		};

		_gem.PlayerHealthThresholdReached += OnPlayerHealthThresholdReached;
		_gem.PlayerHeal += OnPlayerHealReceived;
	}

	private void OnPlayerHealthThresholdReached()
	{
        audioSource.clip = abilityLostSound;
        audioSource.Play();

        IAbility next = _abilities.FirstOrDefault( a => a.IsActive );

		next?.ChangeActivityStatus( false );
	}

	private void OnPlayerHealReceived()
	{
		IAbility next = _abilities.LastOrDefault( a => !a.IsActive );

		next?.ChangeActivityStatus( true );
	}
}