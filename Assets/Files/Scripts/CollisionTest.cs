using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour
{

	private void OnCollisionEnter(Collision hit)
	{
		Debug.Log(hit.transform.name);
	}
}
