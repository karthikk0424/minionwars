using UnityEngine;
using System.Collections;

public class PlayerWeapon : PlayerBase {

	public GameObject PlayerProjectile;
	private ObjectRecycler playerProjectilePool;
	private bool isLAUNCHING = false;

	protected override void InitPlayer ()
	{
		base.InitPlayer ();
		CreateProjectilePool();
	}
	private void CreateProjectilePool()
	{
		//Debug.LogWarning("Player Base start");
		playerProjectilePool = new ObjectRecycler(PlayerProjectile, 10, "_parentPlProjectilePool");

//		TODO: Use easy touch
//		while (true)
//		{
//			if(Input.touchCount == 5)
//			{	playerProjectilePool.DespawnAll();}
//			yield return null;
//		}
	}
	
//	private void OnProjection(Vector3 direction, float force)	
//	{
//		OnStartProjection(direction, force);
//	}

	protected void Despawn(GameObject _go)
	{
		playerProjectilePool.Despawn(_go);
	}

	protected override void HandleOn_SwipeEnd (Gesture gesture)
	{
		base.HandleOn_SwipeEnd (gesture);

		//TODO calculate swipe force
		playerProjectilePool.Spawn();
		PlayerProjectile _projectile =  playerProjectilePool.LastActiveGameObject.GetComponent<PlayerProjectile>();
		if( _projectile != null)
		{
			_projectile.OnProjection(gesture.swipeVector.normalized, 400);
		}
		//OnProjection(gesture.swipeVector.normalized, 400);
		//GameObject.Find("Projectile").GetComponent<Projectile>().MoveInThisDirection(gesture.swipeVector.normalized, forceMag);

		/*
		if(isLAUNCHING == false)
		{
			//SwipeDirection = gesture.swipeVector.normalized;
			isLAUNCHING = true; 
			playerProjectilePool.Spawn(SwipeDirection);
			playerProjectilePool.LastActiveGameObject.rigidbody2D.velocity = 
		}
		*/

		
		//SwipeDirection = gesture.swipeVector.normalized;
		
		//	playerProjectilePool.Spawn(SwipeDirection);
		
		
		/*
		Swipe Length 100- 500 range
		Action Time 0.1 to 1f range
		The lengthier they swipe - the faster it should go. 
		The longer they swipe
		 */
		
		var g = gesture.swipeLength;
		var h = gesture.twistAngle;
		var i = gesture.GetSwipeOrDragAngle();
		var j = gesture.actionTime;
		var k = gesture.deltaPinch;
		var l = gesture.deltaPosition;
		
		Debug.Log(
			"Swipe Length = " + g +
			"\n Twist Angle = " + h +
			"\n Swipe or Drag Angle = " + i +
			"\n Action Time = " + j +
			"\n Delta Pinch = " + k +
			"\n Delta Position = " + l
			);
	}
}
