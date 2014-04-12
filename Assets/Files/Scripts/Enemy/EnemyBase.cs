using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour 
{
	private Vector2 _minbound, _maxBound;

	protected virtual void SpawnHub()
	{
	}

	#region Getters & Setters
	protected Vector3 MinimumBound
	{
		private set{ _minbound = value; }
		get{ return _minbound; }
	}
	
	
	protected Vector3 MaximumBound
	{
		private set{ _maxBound = value; }
		get{ return _maxBound; }
	}
	#endregion

	internal virtual void Awake()
	{
	}

	public void InitiateEnemyBase(Vector2 minimumbounds, Vector2 maximubounds)
	{
		_minbound = minimumbounds;
		_maxBound = maximubounds;
		SpawnHub();
//		_minbound = SceneManager.Instance.MinimumBounds;
//		_maxBound = SceneManager.Instance.MaximumBounds;
	}

	internal virtual void Start()
	{
		//	enemyHubPool = new ObjectRecycler(EnemyHub, 10, "_parentEnemyHubPool");
		//	enemyProjectilePool = new ObjectRecycler(EnemyProjectile, 10, "_parentEnemyProjectilePool");
	}

	internal virtual void OnEnable() {}

	internal virtual void OnDisable() {}

}
