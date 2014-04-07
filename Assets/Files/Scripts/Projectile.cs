using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

	private Vector3 dirToTravel;

	private Rigidbody2D thisRigidBody;
	public float force = 50f;
	
	private void OnEnable()
	{
		dirToTravel = Vector3.zero;
		SceneManager.OnProjection += projectileInvoked;
	//	if(thisRigidBody == null)
	//	{	thisRigidBody = this.GetComponent<Rigidbody2D>();}
	}


	private void OnDisable ()
	{
		SceneManager.OnProjection -= projectileInvoked;
	}
	
	private void projectileInvoked(Vector3 dir, Quaternion forwardAngle)
	{
		this.transform.position = new Vector3(0f,0f,-1f);
		this.transform.rotation = forwardAngle;

		dirToTravel = dir;
	}

	private void Update ()
	{
		this.transform.rigidbody.MovePosition((this.transform.position + (dirToTravel * Time.deltaTime * force)));
	}

	private void OnCollisionEnter(Collision hit)
	{
		if(hit.transform.tag == "SideCollider")
		{
			var preDir = dirToTravel;
			dirToTravel =  Vector3.Reflect(dirToTravel, hit.contacts[0].normal);
		}
	}

	private void OnTriggerEnter (Collider hit)
	{
		Vector3 normalVector = hit.ClosestPointOnBounds(this.transform.position).normalized;
		Debug.Log(normalVector);
		dirToTravel =  Vector3.Reflect(dirToTravel, dirToTravel);
	}
}
