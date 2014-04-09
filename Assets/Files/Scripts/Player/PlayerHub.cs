using UnityEngine;
using System.Collections;

public class PlayerHub : PlayerBase 
{
	public GameObject ProjectilePrefab;
	private ObjectRecycler _playerProjectilePool;

	//Handle SpawnAnimation other events that happens during the initialization
	protected override void InitPlayer ()
	{
		base.InitPlayer ();
		_playerProjectilePool = new ObjectRecycler(ProjectilePrefab,15,gameObject);
		_playerProjectilePool.Spawn(transform.position,Quaternion.identity);

		foreach(Transform t in transform)
		{
			t.gameObject.SetActive(false);
		}
	}
}
