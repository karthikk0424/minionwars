using UnityEngine;
using System.Collections;

public class EnemyHub : EnemyBase 
{
	public GameObject EnemyPrefab;

	private ObjectRecycler enemyHubPool;

	protected override void Spawn()
	{
		base.Spawn();
		enemyHubPool = new ObjectRecycler(EnemyPrefab, 10, gameObject);
		StartCoroutine(this.CreateHub());
	}

	private IEnumerator CreateHub()
	{
		while(true)
		{
			if(enemyHubPool.totalInActive > 0)
			{
				//enemyHubPool.Spawn(returnAvailablePosition(Random.Range(1,5)), Quaternion.identity);
				enemyHubPool.Spawn(returnAvailablePosition(Random.Range(1,5), MinimumBound, MaximumBound), Quaternion.identity);
			}
			yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
		}
	}

	private Vector3 returnAvailablePosition(int sideNum, Vector3 minBounds, Vector3 maxBounds)
	{
		Debug.Log(minBounds);

		// sideNum = Top, Right, Bottom, Left
		switch (sideNum)
		{
			//Top
			case 1:
				//enemy.transform.position = new Vector3(maxBounds.x, Random.Range(minBounds.y, maxBounds.y), -1f);
				return (new Vector3(maxBounds.x, Random.Range(minBounds.y, maxBounds.y), SceneManager.DEFAULT_DEPTH));
				break;

			//Right
			case 2:
			return (new Vector3(Random.Range(minBounds.x, maxBounds.y), maxBounds.y, SceneManager.DEFAULT_DEPTH));
				break;

			//Bottom
			case 3:
			return ( new Vector3(Random.Range(minBounds.x, maxBounds.y), minBounds.y, SceneManager.DEFAULT_DEPTH));
				break;

			//Left
			case 4:
			return ( new Vector3(minBounds.x, Random.Range(minBounds.y, maxBounds.y), SceneManager.DEFAULT_DEPTH));
				break;

			default:
				goto case 1;
				break;
		}
	}
}
