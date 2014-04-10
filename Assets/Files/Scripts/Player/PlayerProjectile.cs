using UnityEngine;
using System.Collections;

public class PlayerProjectile : PlayerBase
{
	private Vector2 directionToMove = Vector2.zero;
	private float forceToApply = 10f;
	private float bounceFactor = 1.0f; //between 0.0f and 1.0f
	
	// Float 0.1f to 1.0f => the more it bounces
	// float 100 to 400f => the more the speed
	private void OnEnable()
	{
	//	directionToMove = base.SwipeDirection;
		directionToMove = this.transform.position;

		forceToApply = SceneManager.Instance.ForceToApply;
		bounceFactor = SceneManager.Instance.BounceFactor;

		this.rigidbody2D.velocity = (directionToMove * (forceToApply * (Time.deltaTime))); 
	}



	/*
	internal void MoveInThisDirection(Vector2 _dir, float _force)
	{
		directionToMove = _dir;
		forceToApply = _force;
		this.rigidbody2D.velocity = (directionToMove * (forceToApply * (Time.deltaTime * 40f))); 
	}
	*/

	private void OnCollisionEnter2D(Collision2D hit)
	{
		switch(hit.gameObject.layer)
		{
			// Side Collider
			case 8:
				Vector2 _normal = hit.contacts[0].normal;
				// Source : http://www.3dkingdoms.com/weekly/weekly.php?a=2
				// Formula = b * ( -2*(V dot N)*N + V )
				directionToMove = (-2 * ((Vector2.Dot(directionToMove, _normal) * _normal)) + directionToMove).normalized;
				this.transform.rigidbody2D.velocity = bounceFactor * (directionToMove * (forceToApply * Time.deltaTime));
				break;

			// Player 
			case 9:
				
				break;

			// Enemy
			case 10:
				
				break;
		}
	}

	internal void DespawnThisGameObject()
	{
		base.Despawn(this.gameObject);
	}
}
