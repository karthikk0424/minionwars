using UnityEngine;
using System.Collections;

public class EnemyHub : EnemyBase 
{

	internal override void Awake()	{}

	internal override void Start()	
	{
		StartCoroutine(this.FireAProjectile());
	//	base.SpawnProjectile(this.transform.localPosition);
	}

	internal override void OnEnable()
	{
		base.UponHubToggle(true);
		Debug.Log("Enemy Hub Enable = " + this.transform.name);
	}

	internal override void OnDisable()
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
		Debug.Log(this.transform.name);

		if(this.gameObject.activeSelf == false)
		{	Debug.LogWarning("Inactive object");}
	}

}
