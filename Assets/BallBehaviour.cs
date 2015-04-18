using UnityEngine;
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
		rigidbody.AddForce (transform.up * 200);
		if (col.gameObject.name == "RespawnPlane")
		{
			print("Game Over");
			Application.LoadLevel (0);
		}
	}

}
