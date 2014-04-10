using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{
	public GameObject PlayerProjectile;
	private ObjectRecycler playerProjectilePool;
	private bool isLAUNCHING = false;

	private Vector2 swipeDirection = Vector2.zero;

	protected Vector2 SwipeDirection
	{
		get{
			Vector2 _temp = swipeDirection;
			swipeDirection = Vector2.zero;
			isLAUNCHING = false;
			return _temp;
		}
	}

	private void Awake()
	{
		Debug.LogWarning("Player Awake");
	}

	private void OnEnable()
	{
		Debug.Log("Player Base OnEnable");
		EasyTouch.On_SwipeEnd += HandleOn_SwipeEnd;
	}

	private IEnumerator Start()
	{
		Debug.Log("Player Base start");
		playerProjectilePool = new ObjectRecycler(PlayerProjectile, 10, "_parentPlProjectilePool");

		while (true)
		{
			if(Input.touchCount == 5)
			{	playerProjectilePool.DespawnAll();}
			yield return null;
		}
	}

	private void OnDisable()
	{
		EasyTouch.On_SwipeEnd -= HandleOn_SwipeEnd;
	}

	protected void Despawn(GameObject _go)
	{
		playerProjectilePool.Despawn(_go);
	}

	protected void UponPlProjectileToggled(bool isACTIVE)
	{
		if(isACTIVE)
		{

		}
		else
		{

		}
	}

	private void HandleOn_SwipeEnd (Gesture gesture)
	{
		//GameObject.Find("Projectile").GetComponent<Projectile>().MoveInThisDirection(gesture.swipeVector.normalized, forceMag);
	/*
		if(isLAUNCHING == false)
		{
			swipeDirection = gesture.swipeVector.normalized;
			isLAUNCHING = true; 
			playerProjectilePool.Spawn(swipeDirection);
		}
		*/

		swipeDirection = gesture.swipeVector.normalized;
	
	//	playerProjectilePool.Spawn(swipeDirection);


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
