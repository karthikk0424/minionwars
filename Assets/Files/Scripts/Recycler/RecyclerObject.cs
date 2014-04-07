using UnityEngine;
using System.Collections;

public class RecyclerObject : MonoBehaviour 
{

	public GameObject EnemyBlob;
	public int NumberOfEnemy;
	private ObjectRecycler enemyRecyle;

	private void Start ()
	{
		enemyRecyle = new ObjectRecycler(EnemyBlob, (uint)NumberOfEnemy);

		//enemyRecyle.Spawn();
	}


	private void Update ()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			enemyRecyle.Spawn();
		}

		if(Input.GetKeyDown(KeyCode.B))
		{
			enemyRecyle.Despawn(GameObject.Find("Enemy(Clone)_3"));
		}
	}
}
