using UnityEngine;
using System.Collections;

public class EnemyWeapon : EnemyBase 
{
	public enum TypeOfUnit
	{
		None,
		Hub,
		Projectile
	};

	public GameObject EnemyProjectilePrefab;
	public GameObject Mouth;
	public TypeOfUnit ThisTypeOfEnemy;

	private GameObject enemyInstance;
	private bool isAnimating;
	private ObjectRecycler _enemyFirePool;

	private void Start()
	{
		_enemyFirePool = new ObjectRecycler(EnemyProjectilePrefab,2,gameObject);
		Shoot();
	}
	protected override void Shoot ()
	{
		base.Shoot ();
		isAnimating = true; 

		if(ThisTypeOfEnemy == TypeOfUnit.None)
		{	
			Debug.LogWarning("Unit Type Not selected yet");
		}
		//Hack
		isAnimating = false; 
	}

	private void OnEnemySpawnAnimationComplete()
	{
		isAnimating = false;
	}

	private void Update()
	{
		if(isAnimating == false)
		{
			ShootPlayer();
		}
	}

	private void ShootPlayer()
	{
		if(_enemyFirePool.totalActive == 0 )
		{
			_enemyFirePool.Spawn(Mouth.transform.position,Quaternion.identity);
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 8)
		{
			//Destroy(gameObject);
		}
	}
//	private void OnCollisionEnter(Collision hit)
//	{
//		if(hit.gameObject.layer == 8)
//		{
//			switch(ThisTypeOfEnemy)
//			{
//				case TypeOfUnit.Hub:
//				this.gameObject.SetActive(false);
//				break;
//			}
//		}
//
//	}

}
