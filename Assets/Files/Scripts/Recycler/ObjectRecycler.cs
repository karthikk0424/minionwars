using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

interface RecyclerContract
{
	int totalPooled
	{	get;}

	int totalActive
	{ 	get;}

	int totalInActive
	{	get;}

	void Spawn();
	void Spawn(Vector3 pos, Quaternion rot);
	void Despawn(GameObject go);
}

public class ObjectRecycler : RecyclerContract
{

	private GameObject theObject, parentGameObject;
	private List<GameObject> recyledObjects = new List<GameObject>();
	private uint totalRecyledObjects = 0;


	#region Getters & Setters

	public int totalPooled
	{
		get {return (int)totalRecyledObjects;}
	}

	public int totalActive
	{
		get {
			return 		(from item in recyledObjects
			             where item.activeSelf == true
			             select item).Count();
		}
	}

	public int totalInActive
	{
		get {
			return (from item in recyledObjects
			        where item.activeSelf == false
			        select item).Count();
		}
	}
	#endregion

	#region Constructors
	internal ObjectRecycler(GameObject go, uint count)
	{
		if((count < 2) || (go == null))
		{	return;}

		parentGameObject = new GameObject((go.transform.name + "_Parent").ToString());

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
	}

	internal ObjectRecycler(GameObject go, uint count,GameObject parent, string parentName)
	{
		if((count < 2) || (go == null))
		{	return;}
		
		parentGameObject = parent;
		parentGameObject.name = parentName;
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
	}
	#endregion


	#region Spawn & Despawn Methods
	public void Spawn(Vector3 worldPosition, Quaternion rot)
	{
		// LINQ to find free objects in the list
		GameObject go =  (from item in recyledObjects
		                  where item.activeSelf == false
		                  select item.gameObject).FirstOrDefault();
		
		if(go == null)
		{
			go  = Object.Instantiate(theObject) as GameObject;
			recyledObjects.Add(go);
			totalRecyledObjects++;
			go.name = (go.name + "_" + totalRecyledObjects);
			go.transform.parent = parentGameObject.transform; 
		}
		
		go.transform.position = worldPosition;
		go.transform.rotation = rot;
		go.SetActive(true);

	}

	public void Spawn()
	{
		// LINQ to find free objects in the list
		GameObject go =  (from item in recyledObjects
		         		 where item.activeSelf == false
		          		 select item.gameObject).FirstOrDefault();

		if(go == null)
		{
			go  = Object.Instantiate(theObject, Vector3.zero, Quaternion.identity) as GameObject;
			recyledObjects.Add(go);
			totalRecyledObjects++;
			go.name = (go.name + "_" + totalRecyledObjects);
			go.transform.parent = parentGameObject.transform; 
		}

		go.transform.position = Vector3.zero;
		go.transform.rotation = Quaternion.identity;
		go.SetActive(true);

	}

	public void Despawn(GameObject go)
	{
		GameObject temp =        (from item in recyledObjects
						   where (item.activeSelf == true && item.gameObject.name == go.name)
		                   select item.gameObject).First();
	
		// Case should be handled when there is no GameObject of 'go'

		if(temp != null)
		{
			temp.transform.position = Vector3.zero;
			temp.transform.rotation = Quaternion.identity;
			temp.SetActive(false);
		}
	}

	
	#endregion

}
