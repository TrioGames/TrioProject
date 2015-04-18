using UnityEngine;
using System.Collections;

public class Gamer : MonoBehaviour {

	private Transform playerTrans;
	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;
	private Vector3 obj1Pos = new Vector3 (-0.6f, -0.48f , -17.0f);
	private Vector3 obj2Pos = new Vector3 (-0.035f , -0.48f, -17.0f);
	private Vector3 obj3Pos = new Vector3 (0.584f , -0.48f, -17.0f);
	public float objScale = 0.75f;


	// Use this for initialization
	void Start () {

			obj1 = GetRandomObject ();
			obj1.transform.position = obj1Pos;

		obj2 = GetRandomObject ();
		obj2.transform.position = obj2Pos;

		obj3 = GetRandomObject ();
		obj3.transform.position = obj3Pos;
	}

	void Awake () {
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		StartGame();
	}
	
	void StartGame()
	{
		Time.timeScale = 1.0f;
	}

	GameObject GetRandomObject()
	{
		int caseSwitch = Random.Range (0,2);
		GameObject obj;
		switch (caseSwitch)
		{
		case 0:
			obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			obj.name = "Cube";
			break;
		case 1:
			obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			obj.name = "Cylinder";
			break;
		case 2:
			obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.name = "Sphere";
			break;
		default:
			obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
			obj.name = "Plane";
			break;
		}
		obj.tag = "Respawn";
		obj.AddComponent("mouseDrag");
		//obj.AddComponent ("ObjectBehavior");
		obj.transform.localScale -= new Vector3(objScale, objScale, objScale);
		return obj;
	}

	public void RecreateMissingObject()
	{
		if (obj1 == null || obj1.name.StartsWith("Destroyed")) {
			//print ("obj1 is null. So recreating...");
			obj1 = GetRandomObject ();
			obj1.transform.position = obj1Pos;
			print ("obj1 is : " + obj1.name );
		}

		if (obj2 == null || obj2.name.StartsWith("Destroyed")) {
			//print ("obj2 is null. So recreating...");
			obj2 = GetRandomObject ();
			obj2.transform.position = obj2Pos;
			print ("obj2 is : " + obj2.name );
		}

		if (obj3 == null || obj3.name.StartsWith("Destroyed")) {
			//print ("obj3 is null. So recreating...");
			obj3 = GetRandomObject ();
			obj3.transform.position = obj3Pos;
			print ("obj3 is : " + obj3.name);
		}
	}


	// Update is called once per frame
	void Update () {
		RecreateMissingObject ();
	}
}
