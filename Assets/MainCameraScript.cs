using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	
	// Use this for initialization
	void Start () 
	{
		//player = GameObject.FindGameObjectWithTag ("Player");
		//offset = transform.position;
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position.y = BallBehaviour.max_height - 10;
		
	}
}
