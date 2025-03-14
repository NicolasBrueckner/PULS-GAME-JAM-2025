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
    public UnityEvent onLoseCondition;

    private Collider2D objectCollider;

    void Start()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
        CheckOverlap();
    }

    private void Move()
    {
        transform.Translate(new Vector2(executionerSpeed, 0) * Time.deltaTime);
        // TODO: Animation oder so?
    }

    private void CheckOverlap()
    {
        if (playerCollider == null || objectCollider == null) return;

        if (playerCollider.bounds.Intersects(objectCollider.bounds))
        {
            Bounds playerBounds = playerCollider.bounds;
            Bounds objectBounds = objectCollider.bounds;

            float xMin = Mathf.Max(playerBounds.min.x, objectBounds.min.x);
            float xMax = Mathf.Min(playerBounds.max.x, objectBounds.max.x);
            float yMin = Mathf.Max(playerBounds.min.y, objectBounds.min.y);
            float yMax = Mathf.Min(playerBounds.max.y, objectBounds.max.y);

            float overlapWidth = Mathf.Max(0, xMax - xMin);
            float overlapHeight = Mathf.Max(0, yMax - yMin);
            float overlapArea = overlapWidth * overlapHeight;

            float objectArea = objectBounds.size.x * objectBounds.size.y;
            float overlapRatio = overlapArea / objectArea;

            if (overlapRatio >= executionerTerminationThreshold)
            {
                onLoseCondition.Invoke();
            }
        }
    }
}
