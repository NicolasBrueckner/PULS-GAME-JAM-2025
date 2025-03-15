#region

using UnityEngine;

#endregion

/// <summary>
///     Terminator nach rechts verschieben und auf Kollision mit Spieler checken
/// </summary>
public class ExecutionerMovement : MonoBehaviour
{
	public int executionerSpeed = 1;
	public float executionerTerminationThreshold = 0.5f;
	public Collider2D playerCollider;

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.Translate( new Vector2( executionerSpeed, 0 ) * Time.deltaTime );
		// TODO: Animation oder so?
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		Debug.Log( "enter" );
	}
}