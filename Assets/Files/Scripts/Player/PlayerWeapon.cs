using UnityEngine;
using System.Collections;

public class PlayerWeapon : PlayerBase {

	private ObjectRecycler _playerProjectilePool;

	protected override void OnProjection(Vector3 dir, Quaternion forwardAngle)
	{
		base.OnProjection(dir,forwardAngle);

		if(transform.childCount > 0 )
		{
			int randomNumber = Random.Range(0,14);
			GameObject instance = transform.GetChild(randomNumber).gameObject;
			instance.SetActive(true);
			instance.GetComponent<Projectile>().Project(dir,forwardAngle);
			//Add changes to ObjectRecycler to toggleGame object
		}
	}
}
