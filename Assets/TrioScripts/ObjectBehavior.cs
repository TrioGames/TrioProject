using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {

	//private int collisionCount = 0;

	private Gamer gamer = new Gamer();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		print ("Camera yukseldi : " + MainCameraScript.CameraInc);
		transform.position = new Vector3(transform.position.x, MainCameraScript.currentCam, transform.position.z);
		//transform.position = new Vector3(transform.position.x, transform.position.y + MainCameraScript.CameraInc, transform.position.z);
	}

	void OnCollisionEnter(Collision col)
	{
		gameObject.name = "Destroyed" + gameObject.name;
		print ("Destroying " + gameObject.name);
		DestroyObject (gameObject);	
	}
	
}
