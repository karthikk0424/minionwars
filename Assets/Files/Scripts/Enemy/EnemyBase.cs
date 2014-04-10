using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour 
{
	private ObjectRecycler enemyHubPool, enemyProjectilePool;

	public GameObject EnemyHub, EnemyProjectile;
	private Vector2 _minbound, _maxBound;

	private int _totalHubsActive = 0, _totalProjectilesActive = 0;

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

	private void Awake()
	{
		Debug.Log("Enemy base awake");
	}

	private void OnEnable()
	{
		Debug.Log("Enemy Base OnEnable");
	}

	private void Start()
	{
		enemyHubPool = new ObjectRecycler(EnemyHub, 10, "_parentEnemyHubPool");
//		enemyProjectilePool = new ObjectRecycler(EnemyProjectile, 10, "_parentEnemyProjectilePool");
		_minbound = SceneManager.Instance.MinimumBounds;
		_maxBound = SceneManager.Instance.MaximumBounds;
		this.StartCoroutine(CreateHub());
		Debug.Log(enemyProjectilePool);
	}

	#region Spawning & Despawning related			
	protected void DespawnHub(GameObject _go)
	{
		enemyHubPool.Despawn(_go);
	}

	protected void DespawnProjectile(GameObject _go)
	{
		enemyProjectilePool.Despawn(_go);
	}

	protected void SpawnProjectile(Vector3 _spawnPosition)
	{
		enemyProjectilePool.Spawn(_spawnPosition);
		var _go = enemyProjectilePool.LastActiveGameObject;
		_go.rigidbody2D.velocity = ((Vector3.zero - _spawnPosition).normalized * 100 * Time.deltaTime);
	}
	
	protected void UponHubToggle(bool toINCREMENT)
	{
		_totalHubsActive += (toINCREMENT) ? (1) : (-1);
	}

	protected void UponProjectileToggle(bool toINCREMENT)
	{
		_totalProjectilesActive += (toINCREMENT) ? (1) : (-1);
	}
	#endregion

	#region Enemy Hub Creation
	private IEnumerator CreateHub()
	{
		while(true)
		{
			if(enemyHubPool.TotalInactive > 0)
			{
				//enemyHubPool.Spawn(returnAvailablePosition(Random.Range(1,5)), Quaternion.identity);
				enemyHubPool.Spawn(returnAvailablePosition(Random.Range(1,5), _minbound, _maxBound));
			}
			
			yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
		}
	}
	#endregion	

	
	private Vector3 returnAvailablePosition(int sideNum, Vector2 minBounds, Vector2 maxBounds)
	{
		
		// sideNum = Top, Right, Bottom, Left
		switch (sideNum)
		{
			//Top
			case 1:
				//enemy.transform.position = new Vector3(maxBounds.x, Random.Range(minBounds.y, maxBounds.y), -1f);
				return (new Vector3(maxBounds.x, Random.Range(minBounds.y, maxBounds.y)));
				break;
				
				//Right
			case 2:
				return (new Vector3(Random.Range(minBounds.x, maxBounds.y), maxBounds.y));
				break;
				
				//Bottom
			case 3:
				return ( new Vector3(Random.Range(minBounds.x, maxBounds.y), minBounds.y));
				break;
				
				//Left
			case 4:
				return ( new Vector3(minBounds.x, Random.Range(minBounds.y, maxBounds.y)));
				break;
				
			default:
				goto case 1;
				break;
		}
	}

}
