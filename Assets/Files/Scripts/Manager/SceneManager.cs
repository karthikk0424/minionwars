using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	public float distance = 10;
	public float goDepth = 4;
	public GameObject Ground;
	public GameObject EnemyHubParent;
	public GameObject PlayerHubParent;//, EnemyPrefab, EnemyHubParent;
	public const float DEFAULT_DEPTH = -2.0F;
	
	private Vector3 minBounds;
	private Vector3 maxBounds;

	Vector3 v3ViewPort;
	Vector3 v3BottomLeft;
	Vector3 v3TopRight;

	Vector3 swipeDirection = new Vector3(0,0,0);
	private Quaternion angleToMove = Quaternion.identity;


	GameObject enemy;
	// Delegate
	public delegate void Projectile(Vector3 dirToProject, Quaternion forwardAngle);	
	public static event Projectile OnProjection;

	// Use this for initialization
	private void Start () 
	{
		ResizeToTheCamera();

		PlayerBase _playerBase = PlayerHubParent.GetComponent<PlayerBase>();
		_playerBase.InitPlayerBase();

		EnemyBase _enemyBase = EnemyHubParent.GetComponent<EnemyBase>();
		_enemyBase.CreateEnemyHub(minBounds,maxBounds);


		//enemyHubPool = new ObjectRecycler(EnemyPrefab, 10, EnemyHubParent);
		// Pooling EnemyUnits
		// Pooling PlayerProjectiles

		//StartCoroutine(this.CreateHub());
	}

	private void OnEnable ()
	{
		if(enemy == null)
		{	enemy = GameObject.Find("Enemy");}

		EasyTouch.On_SwipeEnd += On_SwipeEnd;
	}

	private void OnDisable()
	{
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;
	}


	// Update is called once per frame
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
		//	ReturnFromThisQuadrant(Random.Range(1,5));
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			//ReturnFromThisQuadrant(Random.Range(1,5));
			ResizeToTheCamera();
		}
	}

	/// <summary>
	/// Scales the transform in accordance to the camera's orthographic size
	/// </summary>
	/// <description>
	/// [OrthographicSize] is half of the vertical size of the viewing volume. Horizontal viewing size varies depending on viewport's aspect ratio.
	/// The vertical size of your plane will be twice the orthographic size and then scale the horizontal based on the ratio of Screen.width / Screen.height.
	/// </description>
	/// <seealso cref="http://answers.unity3d.com/questions/444206/scale-plane-to-fit-to-screen-size.html"/>
	private void ResizeToTheCamera()
	{
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		Ground.transform.localScale = new Vector3(width, Mathf.Ceil(width/height), height);
		minBounds = Ground.renderer.bounds.min;
		maxBounds = Ground.renderer.bounds.max;
	}

	private void On_SwipeEnd(Gesture ges)
	{
		var tempVectorTwo = ges.swipeVector.normalized;
//		PlayerBody.velocity = new Vector3(0,0);
		swipeDirection = new Vector3(tempVectorTwo.x, tempVectorTwo.y, 0);

		angleToMove = Quaternion.Euler(new Vector3(0f, 0f, ges.GetSwipeOrDragAngle()));
	//	PlayerBody.AddForce(swipeDirection * 50);
		if(OnProjection != null)
		{
			OnProjection(swipeDirection, angleToMove);
		}


		//	Debug.Log(ges.GetSwipeOrDragAngle());
		//Debug.Log(string.Format("Swipe Vector = (0) \n Swipe Length = {1} \n Twist Angle = {2} \n Pick Object = {3} \n Other Reciever = {4} \n Finger Index = {5} \n Delta Pinch = {6} \n Action Time = {7}",
		 //         	ges.swipeVector.normalized , ges.swipeLength , ges.twistAngle , ges.pickObject , ges.otherReceiver , ges.fingerIndex , ges.deltaPinch , ges.actionTime));
	}
}
