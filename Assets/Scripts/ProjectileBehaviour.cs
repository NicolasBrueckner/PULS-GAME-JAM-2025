using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameplayEventManager.Instance?.OnPlayerHit();
            OnDestroyed();
        }
    }

    private void OnDestroyed()
    {
        gameObject.SetActive(false);
    }
}
