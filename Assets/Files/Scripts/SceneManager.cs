using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

	private Vector2 minBounds, maxBounds;

	public float forceMag = 10; // Prefarbly between 10 to 20
	public GameObject EnemyHub, PlayerProjectile;

	private EnemyBase myEnemyBase;
	private PlayerBase myPlayerBase;


	#region Init Methods
	private void Awake()
	{
		instance = this;
	}
	
	private void OnEnable()
	{
		if(myPlayerBase == null)
		{ myPlayerBase = GameObject.Find("PlayerBase").GetComponent<PlayerBase>() as PlayerBase;}
		if(myEnemyBase == null)
		{	myEnemyBase = GameObject.Find("EnemyBase").GetComponent<EnemyBase>() as EnemyBase;}	
	}

	private IEnumerator Start()
	{
		resizeAccordingDeviceScreen();
		while(true)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{	

			}
			yield return null;
		}
	}


	private void OnDisable()
	{

	}
	
	#endregion

	#region Testing
	private float forceToApply = 100f;
	public float ForceToApply
	{
		get{
			return forceToApply;
		}
	}

	private float bounceFactor = 1.0f;
	public float BounceFactor
	{
		get{
			return bounceFactor;
		}
	}
	#endregion


	#region Singleton
	private static SceneManager instance;
	public static SceneManager Instance
	{
		get{
				if(instance == null)
				{	
					instance = UnityEngine.Object.FindObjectOfType(typeof(SceneManager)) as SceneManager;
					if (instance == null)
					{

						GameObject go = GameObject.Find("Base") as GameObject;
						if(go != null)
						{	instance = go.AddComponent<SceneManager>();}
					}
					if (instance == null)
					{
						GameObject go = new GameObject("Base") as GameObject;
						instance = go.AddComponent<SceneManager>();		
					}
				}
			return instance;
		}
	}
	#endregion

	#region Getters & Setters

	internal Vector2 MinimumBounds
	{
		get{ return minBounds;}
	}

	internal Vector2 MaximumBounds
	{
		get	{ return maxBounds;}
	}

	#endregion


	internal void TestingSingleton()
	{
		Debug.Log("Testing ");
	}

	private void resizeAccordingDeviceScreen()
	{
		float _height = (Camera.main.orthographicSize * 2.0f);
		float _width = _height * Camera.main.aspect; 
		this.transform.localScale = new Vector3((_width * 3.125f), (_height * 3.125f), 1f);
		minBounds = this.renderer.bounds.min;
		maxBounds = this.renderer.bounds.max;
	}

	private void OnGUI()
	{
		GUI.Box(new Rect(50,10,100,50), forceToApply.ToString());
		forceToApply = GUI.HorizontalSlider(new Rect(25, 40, 400, 100), forceToApply, 300f, 1000f);
		bounceFactor = GUI.HorizontalSlider(new Rect(Screen.width - 500, 40, 400, 100), bounceFactor, 0.5f, 1.0f);
		GUI.Box(new Rect((Screen.width - 225),10,100,50), bounceFactor.ToString());

	}
}
