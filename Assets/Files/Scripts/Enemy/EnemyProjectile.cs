using UnityEngine;
using System.Collections;

public class EnemyProjectile : EnemyBase
{
	/*
		1. Movement towards Vector3 Origidin
		2. Collision with projectile 
		3. Trigger with PlayerHub. 
	 */

	internal override void Awake() {}
	internal override void Start() {}
	internal override void OnEnable() {}
	internal override void OnDisable() {}

	private void OnCollisionEnter2D(Collision2D hit)
	{

	}

	private void OnTriggerEnter2D(Collider2D hit)
	{

	}
}
