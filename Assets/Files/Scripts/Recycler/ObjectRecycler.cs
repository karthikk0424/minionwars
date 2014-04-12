using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;

interface RecyclerContract
{
	int TotalRecycled
	{	get;}

	int TotalActive
	{ 	get;}

	int TotalInactive
	{	get;}
	
	GameObject LastActiveGameObject
	{	get;}
	
	void Spawn();
	GameObject Spawn(Vector3 _worldPosition);
	void Spawn(Vector3 _worldPosition, Quaternion _rot);

//	void Spawn(Vector2 _worldPosition);
//	void Spawn(Vector2 _worldPosition, Quaternion _rot);

	void Despawn(GameObject go);
	void DespawnAll();
}

[System.Serializable]
public sealed class ObjectRecycler : RecyclerContract
{

	private GameObject theObject, parentGameObject, lastActiveObject;
	private List<GameObject> recyledObjects = new List<GameObject>();
	private int totalRecyledObjects = 0, totalActiveObjects = 0, totalInactiveObjects = 0;

	#region Getters & Setters

	public int TotalRecycled
	{
		get {	return totalRecyledObjects;}
	}

	public int TotalActive
	{
		get {	return totalActiveObjects;}
	}

	public int TotalInactive
	{
		get {	return totalInactiveObjects;}
	}


	public GameObject LastActiveGameObject
	{
		get { return lastActiveObject;}
	}
	#endregion

	#region Constructors
	internal ObjectRecycler(GameObject go)
	{

	}



	internal ObjectRecycler(GameObject go, uint count)
	{
		if((count < 2) || (go == null))
		{	return;}

		parentGameObject = new GameObject((go.transform.name + "_Parent").ToString());

		theObject = go;
	
		for(int i = 0; i < count; i++)
		{
			GameObject temp  = Object.Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject;
			recyledObjects.Add(temp);
			totalRecyledObjects++;
			temp.name = (temp.name + "_" + totalRecyledObjects);
			temp.transform.parent = parentGameObject.transform;
			temp.SetActive(false);
		}
		RecountTheCounter();
	}

	internal ObjectRecycler(GameObject go, uint count, string parentName)
	{
		if((count < 2) || (go == null))
		{	return;}

		parentGameObject = new GameObject(parentName);
		theObject = go;
		
		for(int i = 0; i < count; i++)
		{
			GameObject temp  = Object.Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject;
			recyledObjects.Add(temp);
			totalRecyledObjects++;
			temp.name = (temp.name + "_" + totalRecyledObjects);
			temp.transform.parent = parentGameObject.transform;
			temp.SetActive(false);
		}
		RecountTheCounter();
	}

	internal ObjectRecycler(GameObject go, uint count, GameObject parent)
	{
		if((count < 2) || (go == null))
		{	return;}
		
		parentGameObject = parent;

		theObject = go;
		
		for(int i = 0; i < count; i++)
		{
			GameObject temp  = Object.Instantiate(theObject, Vector3.zero, Quaternion.identity) as GameObject;
			recyledObjects.Add(temp);
			totalRecyledObjects++;
			temp.name = (temp.name + "_" + totalRecyledObjects);
			temp.transform.parent = parentGameObject.transform;
			temp.SetActive(false);
		}
		RecountTheCounter();
	}

	internal ObjectRecycler(GameObject _go, uint count, GameObject parent, string parentName)
	{
		if((count < 2) || (_go == null))
		{	return;}
		
		parentGameObject = parent;
		parentGameObject.name = parentName;
		theObject = _go;
		
		for(int i = 0; i < count; i++)
		{
			GameObject temp  = Object.Instantiate(theObject, Vector3.zero, Quaternion.identity) as GameObject;
			recyledObjects.Add(temp);
			totalRecyledObjects++;
			temp.name = (temp.name + "_" + totalRecyledObjects);
			temp.transform.parent = parentGameObject.transform;
			temp.SetActive(false);
			
		}
		RecountTheCounter();
	}
	#endregion


	#region Spawn & Despawn Methods
	public void Spawn()
	{
		// LINQ to find free objects in the list
		GameObject _go =  (from item in recyledObjects
		         		 where item.activeSelf == false
		          		 select item.gameObject).FirstOrDefault();

		if(_go == null)
		{
			_go  = Object.Instantiate(theObject, Vector3.zero, Quaternion.identity) as GameObject;
			recyledObjects.Add(_go);
			totalRecyledObjects++;
			_go.name = (_go.name + "_" + totalRecyledObjects);
			_go.transform.parent = parentGameObject.transform; 
		}

		_go.transform.position = Vector3.zero;
		_go.transform.rotation = Quaternion.identity;
		_go.SetActive(true);
		lastActiveObject = _go;
		RecountTheCounter();
	}

	public GameObject Spawn(Vector3 _worldPosition)
	{
		// LINQ to find free objects in the list
		GameObject _go =  (from item in recyledObjects
		                   where item.activeSelf == false
		                   select item.gameObject).FirstOrDefault();
		
		if(_go == null)
		{
			_go  = Object.Instantiate(theObject) as GameObject;
			recyledObjects.Add(_go);
			totalRecyledObjects++;
			_go.name = (_go.name + "_" + totalRecyledObjects);
			_go.transform.parent = parentGameObject.transform; 
		}
		
		_go.transform.position = _worldPosition;
		_go.transform.rotation = Quaternion.identity;
		_go.SetActive(true);
		RecountTheCounter();
		return _go;
	}


	public void Spawn(Vector3 _worldPosition, Quaternion _rot)
	{
		// LINQ to find free objects in the list
		GameObject _go =  (from item in recyledObjects
		                   where item.activeSelf == false
		                   select item.gameObject).FirstOrDefault();
		
		if(_go == null)
		{
			_go  = Object.Instantiate(theObject) as GameObject;
			recyledObjects.Add(_go);
			totalRecyledObjects++;
			_go.name = (_go.name + "_" + totalRecyledObjects);
			_go.transform.parent = parentGameObject.transform; 
		}
		
		_go.transform.position = _worldPosition;
		_go.transform.rotation = _rot;
		_go.SetActive(true);
		lastActiveObject = _go;
		RecountTheCounter();
	}

	public void Despawn(GameObject _go)
	{
		if(_go != null && _go.activeSelf == true)
		{
			_go.transform.position = Vector3.zero;
			_go.transform.rotation = Quaternion.identity;
			_go.SetActive(false);
		}

		RecountTheCounter();
	}

	public void DespawnAll()
	{
		foreach(GameObject item in recyledObjects)
		{
			if(item.activeSelf == true)
			{	Despawn(item);}
		}
	}

	private void RecountTheCounter()
	{
		totalActiveObjects = (from item in recyledObjects
							 where item.activeSelf == true
							 select item).Count();

		totalInactiveObjects = (from item in recyledObjects
		                        where item.activeSelf == false
		                        select item).Count();	 
	}
	#endregion

}



/*


[System.Serializable]
public sealed class TrashManRecycleBin
{
	/// <summary>
	/// Fired when the GameObject that was just spawned
	/// </summary>
	public event Action<GameObject> onSpawnedEvent;
	
	/// <summary>
	/// Fired when the GameObject that was just despawned
	/// </summary>
	public event Action<GameObject> onDespawnedEvent;
	
	
	/// <summary>
	/// The prefab or GameObject in the scene managed by this class.
	/// </summary>
	public GameObject prefab;
	
	/// <summary>
	/// total number of instances to create at start
	/// </summary>
	public int instancesToPreallocate = 5;
	
	/// <summary>
	/// total number of instances to allocate if one is requested when the bin is empty
	/// </summary>
	public int instancesToAllocateIfEmpty = 1;
	
	/// <summary>
	/// if true, the recycle bin will not create any more instances when hardLimit is reached and will instead return null for any spanws
	/// </summary>
	public bool imposeHardLimit = false;
	
	/// <summary>
	/// if imposeHardLimit is true, this will be the maximum number of instances to create
	/// </summary>
	public int hardLimit = 5;
	
	/// <summary>
	/// if true, any excess instances will be culled at regular intervals
	/// </summary>
	public bool cullExcessPrefabs = false;
	
	/// <summary>
	/// total instances to keep in the pool. All excess will be culled if cullExcessPrefabs is true
	/// </summary>
	public int instancesToMaintainInPool = 5;
	
	/// <summary>
	/// how often in seconds should culling occur
	/// </summary>
	public float cullInterval = 10f;
	
	
	/// <summary>
	/// stores all of our GameObjects
	/// </summary>
	private Stack<GameObject> _gameObjectPool;
	
	/// <summary>
	/// last time culling happened
	/// </summary>
	private float _timeOfLastCull = float.MinValue;
	
	/// <summary>
	/// keeps track of the total number of instances spawned
	/// </summary>
	private int _spawnedInstanceCount = 0;
	
	
	#region Private
	
	/// <summary>
	/// allocates
	/// </summary>
	/// <param name="count">Count.</param>
	private void allocateGameObjects( int count )
	{
		if( imposeHardLimit && _gameObjectPool.Count + count > hardLimit )
			count = hardLimit - _gameObjectPool.Count;
		
		for( int n = 0; n < count; n++ )
		{
			GameObject go = GameObject.Instantiate( prefab.gameObject ) as GameObject;
			go.name = prefab.name;
			go.transform.parent = TrashMan.instance.transform;
			go.SetActive( false );
			_gameObjectPool.Push( go );
		}
	}
	
	
	/// <summary>
	/// pops an object off the stack. Returns null if we hit the hardLimit.
	/// </summary>
	private GameObject pop()
	{
		if( imposeHardLimit && _spawnedInstanceCount >= hardLimit )
			return null;
		
		if( _gameObjectPool.Count > 0 )
		{
			_spawnedInstanceCount++;
			return _gameObjectPool.Pop();
		}
		
		allocateGameObjects( instancesToAllocateIfEmpty );
		return pop();
	}
	
	#endregion
	
	
	#region Public
	
	/// <summary>
	/// preps the Stack and does preallocation
	/// </summary>
	public void initialize()
	{
		//prefab.prefabPoolName = prefab.gameObject.name;
		_gameObjectPool = new Stack<GameObject>( instancesToPreallocate );
		allocateGameObjects( instancesToPreallocate );
	}
	
	
	/// <summary>
	/// culls any excess objects if necessary
	/// </summary>
	public void cullExcessObjects()
	{
		if( !cullExcessPrefabs || _gameObjectPool.Count <= instancesToMaintainInPool )
			return;
		
		if( Time.time > _timeOfLastCull + cullInterval )
		{
			_timeOfLastCull = Time.time;
			for( int n = instancesToMaintainInPool; n <= _gameObjectPool.Count; n++ )
				GameObject.Destroy( _gameObjectPool.Pop() );
		}
	}
	
	
	/// <summary>
	/// fetches a new instance from the recycle bin. Returns null if we reached the hardLimit.
	/// </summary>
	public GameObject spawn()
	{
		var go = pop();
		
		if( go != null )
		{
			if( onSpawnedEvent != null )
				onSpawnedEvent( go );
		}
		
		return go;
	}
	
	
	/// <summary>
	/// returns an instance to the recycle bin
	/// </summary>
	/// <param name="go">Go.</param>
	public void despawn( GameObject go )
	{
		go.SetActive( false );
		
		_spawnedInstanceCount--;
		_gameObjectPool.Push( go );
		
		if( onDespawnedEvent != null )
			onDespawnedEvent( go );
	}
	
	#endregion
	
}



public partial class TrashMan : MonoBehaviour
{
	/// <summary>
	/// access to the singleton
	/// </summary>
	public static TrashMan instance;
	
	/// <summary>
	/// stores the recycle bins and is used to populate the Dictionaries at startup
	/// </summary>
	public List<TrashManRecycleBin> recycleBinCollection;
	
	/// <summary>
	/// uses the GameObject instanceId as its key for fast look-ups
	/// </summary>
	private Dictionary<int,TrashManRecycleBin> _instanceIdToRecycleBin = new Dictionary<int,TrashManRecycleBin>();
	
	/// <summary>
	/// uses the pool name to find the GameObject instanceId
	/// </summary>
	private Dictionary<string,int> _poolNameToInstanceId = new Dictionary<string,int>();
	
	[HideInInspector]
	public new Transform transform;
	
	
	#region MonoBehaviour
	
	private void Awake()
	{
		if( instance != null )
		{
			Destroy( gameObject );
		}
		else
		{
			transform = gameObject.transform;
			instance = this;
			initializePrefabPools();
		}
		
		StartCoroutine( cullExcessObjects() );
	}
	
	
	// TODO: perhaps make this configurable per pool then add DontDestroyOnLoad. Currently this does nothing.
	//	private void OnLevelWasLoaded()
	//	{}
	
	
	private void OnApplicationQuit()
	{
		instance = null;
	}
	
	#endregion
	
	
	#region Private
	
	/// <summary>
	/// coroutine that runs every couple seconds and removes any objects created over the recycle bins limit
	/// </summary>
	/// <returns>The excess objects.</returns>
	private IEnumerator cullExcessObjects()
	{
		var waiter = new WaitForSeconds( 5f );
		
		while( true )
		{
			for( var i = 0; i < recycleBinCollection.Count; i++ )
				recycleBinCollection[i].cullExcessObjects();
			
			yield return waiter;
		}
	}
	
	
	/// <summary>
	/// populats the lookup dictionaries
	/// </summary>
	private void initializePrefabPools()
	{
		if( recycleBinCollection == null )
			return;
		
		foreach( var recycleBin in recycleBinCollection )
		{
			if( recycleBin == null || recycleBin.prefab == null )
				continue;
			
			recycleBin.initialize();
			_instanceIdToRecycleBin.Add( recycleBin.prefab.GetInstanceID(), recycleBin );
			_poolNameToInstanceId.Add( recycleBin.prefab.name, recycleBin.prefab.GetInstanceID() );
		}
	}
	
	
	/// <summary>
	/// internal method that actually does the work of grabbing the item from the bin and returning it
	/// </summary>
	/// <param name="gameObjectInstanceId">Game object instance identifier.</param>
	private static GameObject spawn( int gameObjectInstanceId, Vector3 position, Quaternion rotation )
	{
		if( instance._instanceIdToRecycleBin.ContainsKey( gameObjectInstanceId ) )
		{
			var newGo = instance._instanceIdToRecycleBin[gameObjectInstanceId].spawn();
			
			if( newGo != null )
			{
				var newTransform = newGo.transform;
				newTransform.parent = null;
				newTransform.position = position;
				newTransform.rotation = rotation;
				
				newGo.SetActive( true );
			}
			
			return newGo;
		}
		
		return null;
	}
	
	
	/// <summary>
	/// internal coroutine for despawning after a delay
	/// </summary>
	/// <returns>The despawn after delay.</returns>
	/// <param name="go">Go.</param>
	/// <param name="delayInSeconds">Delay in seconds.</param>
	private IEnumerator internalDespawnAfterDelay( GameObject go, float delayInSeconds )
	{
		yield return new WaitForSeconds( delayInSeconds );
		despawn( go );
	}
	
	#endregion
	
	
	#region Public
	
	public static void manageRecycleBin( TrashManRecycleBin recycleBin )
	{
		// make sure we can safely add the bin!
		if( instance._poolNameToInstanceId.ContainsKey( recycleBin.prefab.name ) )
		{
			Debug.LogError( "Cannot manage the recycle bin because there is already a GameObject with the name (" + recycleBin.prefab.name + ") being managed" );
			return;
		}
		
		instance.recycleBinCollection.Add( recycleBin );
		recycleBin.initialize();
		instance._instanceIdToRecycleBin.Add( recycleBin.prefab.GetInstanceID(), recycleBin );
		instance._poolNameToInstanceId.Add( recycleBin.prefab.name, recycleBin.prefab.GetInstanceID() );
	}
	
	
	/// <summary>
	/// pulls an object out of the recycle bin
	/// </summary>
	/// <param name="go">Go.</param>
	public static GameObject spawn( GameObject go, Vector3 position = default( Vector3 ), Quaternion rotation = default( Quaternion ) )
	{
		if( instance._instanceIdToRecycleBin.ContainsKey( go.GetInstanceID() ) )
		{
			return spawn( go.GetInstanceID(), position, rotation );
		}
		else
		{
			Debug.LogError( "attempted to spawn go (" + go.name + ") but there is no recycle bin setup for it. Falling back to Instantiate" );
			var newGo = GameObject.Instantiate( go, position, rotation ) as GameObject;
			newGo.transform.parent = null;
			
			return newGo;
		}
	}
	
	
	/// <summary>
	/// pulls an object out of the recycle bin using the bin's name
	/// </summary>
	public static GameObject spawn( string gameObjectName, Vector3 position = default( Vector3 ), Quaternion rotation = default( Quaternion ) )
	{
		int instanceId = -1;
		if( instance._poolNameToInstanceId.TryGetValue( gameObjectName, out instanceId ) )
		{
			return spawn( instanceId, position, rotation );
		}
		else
		{
			Debug.LogError( "attempted to spawn a GameObject from recycle bin (" + gameObjectName + ") but there is no recycle bin setup for it" );
			return null;
		}
	}
	
	
	/// <summary>
	/// sticks the GameObject back into it's recycle bin. If the GameObject has no bin it is destroyed.
	/// </summary>
	/// <param name="go">Go.</param>
	public static void despawn( GameObject go )
	{
		if( go == null )
			return;
		
		var goName = go.name;
		if( !instance._poolNameToInstanceId.ContainsKey( goName ) )
		{
			Destroy( go );
		}
		else
		{
			instance._instanceIdToRecycleBin[instance._poolNameToInstanceId[goName]].despawn( go );
			go.transform.parent = instance.transform;
		}
	}
	
	
	/// <summary>
	/// sticks the GameObject back into it's recycle bin after a delay. If the GameObject has no bin it is destroyed.
	/// </summary>
	/// <param name="go">Go.</param>
	public static void despawnAfterDelay( GameObject go, float delayInSeconds )
	{
		if( go == null )
			return;
		
		instance.StartCoroutine( instance.internalDespawnAfterDelay( go, delayInSeconds ) );
	}
	
	
	/// <summary>
	/// gets the recycle bin for the given GameObject name. Returns null if none exists.
	/// </summary>
	public static TrashManRecycleBin recycleBinForGameObjectName( string gameObjectName )
	{
		if( instance._poolNameToInstanceId.ContainsKey( gameObjectName ) )
		{
			var instanceId = instance._poolNameToInstanceId[gameObjectName];
			return instance._instanceIdToRecycleBin[instanceId];
		}
		return null;
	}
	
	
	/// <summary>
	/// gets the recycle bin for the given GameObject. Returns null if none exists.
	/// </summary>
	/// <returns>The bin for game object.</returns>
	/// <param name="go">Go.</param>
	public static TrashManRecycleBin recycleBinForGameObject( GameObject go )
	{
		if( instance._instanceIdToRecycleBin.ContainsKey( go.GetInstanceID() ) )
			return instance._instanceIdToRecycleBin[go.GetInstanceID()];
		return null;
	}
	
	
	#endregion
	
}
*/
