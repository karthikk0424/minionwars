using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour 
{
	public enum TypeOfUnit
	{
		None,
		Hub,
		Projectile
	};
	public TypeOfUnit ThisTypeOfEnemy;

	private void Start()
	{
		if(ThisTypeOfEnemy == TypeOfUnit.None)
		{	Debug.LogWarning("Unit Type Not selected yet");}



	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("ddd");
		if(other.gameObject.layer == 8)
		{
			Destroy(gameObject);
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
