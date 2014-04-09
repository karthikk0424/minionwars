using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	private Vector3 dirToTravel = Vector3.zero;

	private Rigidbody2D thisRigidBody;
	public float force = 50f;
	private bool project = false;
	private void OnEnable()
	{
		//SceneManager.OnProjection += projectileInvoked;
	//	if(thisRigidBody == null)
	//	{	thisRigidBody = this.GetComponent<Rigidbody2D>();}
	}

//	private void OnDisable ()
//	{
//		//SceneManager.OnProjection -= projectileInvoked;
//	}
	public void Project(Vector3 dir, Quaternion forwardAngle)
	{
		this.transform.position = new Vector3(0f,0f,SceneManager.DEFAULT_DEPTH);
		this.transform.rotation = forwardAngle;

		dirToTravel = dir;
		project = true;
	}

	private void Update ()
	{
		if(project = true)
		{
			//transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position,Time.deltaTime * 2);
			this.rigidbody.MovePosition((this.rigidbody.position + (dirToTravel * Time.deltaTime * force)));
			//rigidbody.MovePosition(rigidbody.position + dirToTravel * Time.deltaTime * force);
			//rigidbody.MovePosition(rigidbody.position + dirToTravel * Time.deltaTime);
		}
	}

	private void OnCollisionEnter(Collision hit)
	{
		if(hit.transform.tag == "SideCollider")
		{
			var preDir = dirToTravel;
			dirToTravel =  Vector3.Reflect(dirToTravel, hit.contacts[0].normal);
		}
	}
//
//	private void OnTriggerEnter (Collider hit)
//	{
//		Vector3 normalVector = hit.ClosestPointOnBounds(this.transform.position).normalized;
//		Debug.Log(normalVector);
//		dirToTravel =  Vector3.Reflect(dirToTravel, dirToTravel);
//	}
}
