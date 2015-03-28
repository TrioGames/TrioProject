using UnityEngine;
using System.Collections;

public class destroyobject : MonoBehaviour {

	public float yokolmasaniye;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, yokolmasaniye);
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	
	}
}
