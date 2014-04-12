using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabFactory : MonoBehaviour
{
	public static PrefabFactory PrefabFactoryInstance = null;

	public static Vector3 GetScaledObjec(GameObject prefab)
	{
		Vector3 scale = new Vector3((Screen.width * prefab.transform.localScale.x)/ StaticContainer.REFERENCE_RESOLUTION, 
		                                          (Screen.width * prefab.transform.localScale.x)/ StaticContainer.REFERENCE_RESOLUTION, 
		                                      1);
		return scale;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(100, 100, 100, 100), Screen.width.ToString());
	}
}
