using UnityEngine;
using System.Collections;

public class EnemyHub : EnemyBase 
{
	public GameObject EnemyHubPrefab, EnemyProjectile;
	private int _totalHubsActive = 0, _totalProjectilesActive = 0;
	private ObjectRecycler enemyProjectilePool,enemyHubPool;

	protected override void SpawnHub()
	{
		enemyHubPool = new ObjectRecycler(EnemyHubPrefab, 10, "_parentEnemyHubPool");
		enemyProjectilePool = new ObjectRecycler(EnemyProjectile, 10, "_parentEnemyProjectilePool");
		this.StartCoroutine(CreateHub());
	}

	internal override void Awake()	
	{
		base.Awake();
		#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
		EnemyHubPrefab.transform.localScale = PrefabFactory.GetScaledObjec(EnemyHubPrefab);
		#endif
//		Debug.LogWarning("Enemy Hub Awake");
//		enemyHubPool = new ObjectRecycler(EnemyHubPrefab, 10, "_parentEnemyHubPool");
//		enemyProjectilePool = new ObjectRecycler(EnemyProjectile, 10, "_parentEnemyProjectilePool");
	}

	internal override void Start()	
	{
//		StartCoroutine(this.FireAProjectile());
//	//	base.SpawnProjectile(this.transform.localPosition);
//		Debug.LogWarning("Enemy Hub Start");
//		this.StartCoroutine(CreateHub());
	}

	internal override void OnEnable()
	{
		UponHubToggle(true);
		Debug.Log("Enemy Hub Enable = " + this.transform.name);
	}

	internal override void OnDisable()
	{
		UponHubToggle(false);
	}

	private void OnTriggerEnter2D(Collider2D hit)
	{
		// Player Projectile
		if(hit.gameObject.layer == 9)
		{
			if(hit.GetComponent<PlayerProjectile>() != null)
			{
				hit.GetComponent<PlayerProjectile>().DespawnThisGameObject();
			}
			this.transform.position = Vector2.zero;
			this.transform.rotation = Quaternion.identity;
			DespawnHub(this.gameObject);
		}
	}

	private IEnumerator FireAProjectile()
	{
		yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
		SpawnProjectile(this.transform.localPosition);
		Debug.Log(this.transform.name);

		if(this.gameObject.activeSelf == false)
		{	Debug.LogWarning("Inactive object");}
	}
	
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


	#region Enemy Hub Creation
	private IEnumerator CreateHub()
	{
		while(true)
		{
			if(enemyHubPool.TotalInactive > 0)
			{
				//enemyHubPool.Spawn(returnAvailablePosition(Random.Range(1,5)), Quaternion.identity);
				Vector3 position = returnAvailablePosition(Random.Range(1,5), MinimumBound, MaximumBound);
				enemyHubPool.Spawn(position, Quaternion.identity);
			}
			
			yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
		}
	}
	#endregion	

	#region Spawning & Despawning related			
	private void DespawnHub(GameObject _go)
	{
		enemyHubPool.Despawn(_go);
	}
	
	protected void DespawnProjectile(GameObject _go)
	{
		enemyProjectilePool.Despawn(_go);
	}
	
	private void SpawnProjectile(Vector3 _spawnPosition)
	{
		//	Debug.LogWarning(enemyHubPool);
		
		GameObject _go = enemyProjectilePool.Spawn(_spawnPosition);
		//		var _go = enemyProjectilePool.LastActiveGameObject;
		Debug.LogWarning(_go);
		_go.rigidbody2D.velocity = ((Vector3.zero - _spawnPosition).normalized * 100 * Time.deltaTime);
	}
	
	private void UponHubToggle(bool toINCREMENT)
	{
		_totalHubsActive += (toINCREMENT) ? (1) : (-1);
	}
	
	protected void UponProjectileToggle(bool toINCREMENT)
	{
		_totalProjectilesActive += (toINCREMENT) ? (1) : (-1);
	}
	#endregion

}
