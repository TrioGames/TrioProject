using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {

	//private int collisionCount = 0;

	public static float defaultZ = -17.0f;
	
	private float initHeight;
	
	// Use this for initialization
	void Start () {
		initHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float currentObjectHeight = initHeight + MainCameraScript.CameraInc;
		if (transform.position.z != defaultZ)
			transform.position = transform.position = new Vector3(transform.position.x, currentObjectHeight, transform.position.z);;
	}

	void OnCollisionEnter(Collision col)
	{
		//print ("Destroying " + gameObject.name);
		gameObject.name = "Destroyed" + gameObject.name;
		DestroyObject (gameObject);	
	}
	
}
