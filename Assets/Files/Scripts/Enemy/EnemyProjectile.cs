using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour 
{
	private GameObject target;

	private void OnEnable()
	{
		target = GameObject.FindGameObjectWithTag(StaticContainer.PLAYER);
	}
	private void Update()
	{
		float move = 1.5F * Time.deltaTime;
		Vector3 direction = target.transform.position - transform.position;
		transform.Translate(direction * move);
	}
}
