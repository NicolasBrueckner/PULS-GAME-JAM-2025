using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Terminator nach rechts verschieben und auf Kollision mit Spieler checken
/// </summary>
public class ExecutionerMovement : MonoBehaviour
{
    public int executionerSpeed = 1;
    public float executionerTerminationThreshold = 0.5f;
    public Collider2D playerCollider;

    private Collider2D objectCollider;

    void Start()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector2(executionerSpeed, 0) * Time.deltaTime);
        // TODO: Animation oder so?
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        GameplayEventManager.Instance?.OnPlayerExecute();
    }
}
