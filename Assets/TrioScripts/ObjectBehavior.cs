using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {

	//private int collisionCount = 0;

	public static float defaultZ = -17.0f;

	private Gamer gamer = new Gamer();

	private float initHeight;

	private bool dragged;

	// Use this for initialization
	void Start () {
		initHeight = transform.position.y;
		dragged = false;
	}
	
	// Update is called once per frame
	void Update () {
		print ("Camera yukseldi : " + MainCameraScript.CameraInc);
		if (transform.position.z != defaultZ)
			transform.position = new Vector3(transform.position.x, initHeight + MainCameraScript.CameraInc, transform.position.z);
	}

	void OnCollisionEnter(Collision col)
	{
		gameObject.name = "Destroyed" + gameObject.name;
		print ("Destroying " + gameObject.name);
		DestroyObject (gameObject);	
	}
	
}
