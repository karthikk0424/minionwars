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
	}


	private void OnGUI()
	{

	//	GUI.Box(new Rect(50,10,80,30), (" Active = " + enemyRecyle.TotalActive.ToString()));

//		GUI.Box(new Rect(50,60,80,30), ("Inactive = " +enemyRecyle.TotalInactive.ToString()));

//		GUI.Box(new Rect(50,110,80,30), ("Pooled = " +enemyRecyle.TotalRecycled.ToString()));

		// SLIDER forceMag = GUI.HorizontalSlider(new Rect(25, 40, 150, 30), forceMag, 1.0f, 20.0f);
	}
}
