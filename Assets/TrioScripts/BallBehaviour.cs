﻿using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	public static float max_height = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y > max_height) {
			max_height = gameObject.transform.position.y;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		//if (col.gameObject.tag == "TrioObject" || col.gameObject.tag == "Platform") {
		if (col.gameObject.tag == "TrioObject") {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, rigidbody.velocity.z); 
		}

		if (col.gameObject.name == "RespawnPlane")
		{
			print("Game Over");
			Application.LoadLevel (0);
		}
	}

}
