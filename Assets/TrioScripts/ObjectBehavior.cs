using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {

	//private int collisionCount = 0;

	public static float defaultZ = -17.0f;
	
	private float initHeight;

	private Transform planeTrans;
	
	// Use this for initialization
	void Start () {
		initHeight = transform.position.y;
		planeTrans = GameObject.FindGameObjectWithTag (Constants.TAG_BOTTOM_PLANE).transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float currentObjectHeight = initHeight + MainCameraScript.CameraInc;
		if (transform.position.z != defaultZ)
			transform.position = new Vector3(transform.position.x, currentObjectHeight, transform.position.z);
	}

	void Update() {
		planeTrans = GameObject.FindGameObjectWithTag (Constants.TAG_BOTTOM_PLANE).transform;
		if (transform.position.y < planeTrans.position.y) 
		{					
			Destroy (gameObject);
		}    
	}

	void OnCollisionEnter(Collision col)
	{
		gameObject.name = "Destroyed" + gameObject.name;
		DestroyObject (gameObject);	
	}

	void OnTriggerEnter(Collider col)
	{
		print ("OnTrigger");
		if (col.gameObject.name.Equals("RockBottomPlane")) {
			DestroyObject (gameObject);	
		}
	}
	
}
