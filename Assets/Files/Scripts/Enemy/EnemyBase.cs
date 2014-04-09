using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	protected virtual void Shoot(){}
	protected virtual void Spawn(){}
	
	#region Enemy Hub Creation
	public void CreateEnemyHub(Vector3 minBound, Vector3 maxbound)
	{
		MinimumBound = minBound;
		MaximumBound = maxbound;
		Spawn();
	}

	#endregion

	/*
	 * Setters and Getters
	 */
	private Vector3 _minbound;
	protected Vector3 MinimumBound
	{
		private set{ _minbound = value; }
		get{ return _minbound; }
	}

	private Vector3 _maxBound;
	protected Vector3 MaximumBound
	{
		private set{ _maxBound = value; }
		get{ return _maxBound; }
	}
}
