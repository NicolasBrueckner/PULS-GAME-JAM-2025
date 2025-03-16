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

    private void OnTriggerStay(Collider other)
    {
        if (!IsInLayerMask(other.gameObject, _playerLayer))
            return;

        if (!audioSourceDamage.isPlaying)
        {
            audioSourceDamage.clip = damageSound;
            audioSourceDamage.Play();
        }

        // should give continous dmg but wait for invincible
        _gem.OnPlayerHit();
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsInLayerMask(other.gameObject, _playerLayer))
        {
            audioSourceDamage.Stop();
        }
    }
}
