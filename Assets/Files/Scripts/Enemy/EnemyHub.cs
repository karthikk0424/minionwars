using UnityEngine;
using System.Collections;

public class EnemyHub : EnemyBase 
{

	private void OnEnable()
	{
		base.UponHubToggle(true);
	//	StartCoroutine(this.FireAProjectile());
	}

	private void OnDisable()
	{
		base.UponHubToggle(false);
	}

	private void OnTriggerEnter2D(Collider2D hit)
	{
		// Player Projectile
		if(hit.gameObject.layer == 9)
		{
			if(hit.GetComponent<PlayerProjectile>() != null)
			{
				hit.GetComponent<PlayerProjectile>().DespawnThisGameObject();
			}
			this.transform.position = Vector2.zero;
			this.transform.rotation = Quaternion.identity;
			base.DespawnHub(this.gameObject);
		}
	}

	private IEnumerator FireAProjectile()
	{
		yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
		base.SpawnProjectile(this.transform.localPosition);
		if(this.gameObject.activeSelf == false)
		{	Debug.LogWarning("Inactive object");}
	}

}
