#region

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class ProjectileShooter : MonoBehaviour
{
	public GameObject projectilePrefab;
	public float frequency = 1f;

	private Queue<GameObject> _projectilePool;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_gem.ProjectileDeactivate += EnqueueProjectile;

		InitializePool();
		StartCoroutine( ShootCoroutine() );
	}

	private void EnqueueProjectile( GameObject obj )
	{
		Debug.Log( "EnqueueProjectile" );
		obj.SetActive( false );
		obj.transform.position = transform.position;
		_projectilePool.Enqueue( obj );
	}

	private void InitializePool()
	{
		_projectilePool = new Queue<GameObject>();

		for( int i = 0; i < 10; i++ )
		{
			GameObject proj = Instantiate( projectilePrefab, transform.position, Quaternion.identity, transform );

			proj.SetActive( false );
			_projectilePool.Enqueue( proj );
		}
	}

	private IEnumerator ShootCoroutine()
	{
		while( enabled )
		{
			ShootNextProjectile();
			yield return new WaitForSeconds( frequency );
		}
	}

	private void ShootNextProjectile()
	{
		GameObject next;

		if( _projectilePool.Count > 0 )
		{
			next = _projectilePool.Dequeue();
		}
		else
		{
			next = Instantiate( projectilePrefab, transform.position, Quaternion.identity, transform );
			_projectilePool.Enqueue( next );
		}

		ActivateProjectile( next );
	}

	private static void ActivateProjectile( GameObject next )
	{
		ProjectileBehaviour nextProjectile = next.GetComponent<ProjectileBehaviour>();

		next.SetActive( true );
		nextProjectile.StartFlying();
	}
}