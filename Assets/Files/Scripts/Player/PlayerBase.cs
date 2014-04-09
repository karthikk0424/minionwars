using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour 
{
	protected virtual void InitPlayer() {}
	protected virtual void OnProjection(Vector3 direction, Quaternion rotation)	{}

	public void InitPlayerBase()
	{
		this.InitPlayer();
	}
	private void OnEnable()
	{
		SceneManager.OnProjection += OnProjection;
	}
	
	private void OnDisable ()
	{
		SceneManager.OnProjection -= OnProjection;
	}
}
