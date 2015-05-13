using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "mainball")
		{
			DestroyObject (gameObject);	
		}
	}
}
