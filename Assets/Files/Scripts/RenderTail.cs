using UnityEngine;
using System.Collections;

public class RenderTail : MonoBehaviour 
{

	private LineRenderer thisRenderer;
	private Transform MyPlayer;

	private void Awake()
	{
		MyPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	private void Update ()
	{

	}
}
