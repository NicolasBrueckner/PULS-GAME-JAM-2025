using UnityEngine;
using static Kaputt;

public class DamageZoneBheaviour : MonoBehaviour
{
    public AudioClip damageSound;
    public AudioSource audioSourceDamage;

    private LayerMask _playerLayer;

    private GameplayEventManager _gem => GameplayEventManager.Instance;

    private void Awake()
    {
        _playerLayer = 1 << LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsInLayerMask(other.gameObject, _playerLayer))
            return;

        audioSourceDamage.clip = damageSound;
        audioSourceDamage.Play();

        _gem.OnPlayerHit();
    }
}
