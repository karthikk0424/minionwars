using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{
	protected virtual void InitPlayer() {}
	//protected virtual void OnProjection(Vector3 direction, Quaternion rotation)	{}
	protected virtual void HandleOn_SwipeEnd (Gesture gesture)
	{
		swipeDirection = gesture.swipeVector.normalized;
	}
	private Vector2 swipeDirection = Vector2.zero;

	public void InitiatePlayerStation()
	{
		InitPlayer();
	}

	protected Vector2 SwipeDirection
	{
		get{
			Vector2 _temp = swipeDirection;
			swipeDirection = Vector2.zero;
			return _temp;
		}
	}

	private void Awake()
	{
		gameObject.transform.localScale = PrefabFactory.GetScaledObjec(gameObject);
		//Debug.LogWarning("Player Awake");
	}

	private void OnDisable()
	{
		EasyTouch.On_SwipeEnd -= HandleOn_SwipeEnd;
	}

	private void OnEnable()
	{
		//Debug.LogWarning("Player Base OnEnable");
		EasyTouch.On_SwipeEnd += HandleOn_SwipeEnd;
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


}
