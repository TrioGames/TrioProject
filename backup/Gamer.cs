using UnityEngine;
using System.Collections;

public class Gamer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obj = GetRandomObject();
		obj.transform.position = new Vector3(0, 0, -17);
		GameObject obj2 = GetRandomObject();
		obj2.transform.position = new Vector3(2, 0, -17);
		GameObject obj3 = GetRandomObject();
		obj3.transform.position = new Vector3(4, 0, -17);

	}

	GameObject GetRandomObject()
	{
		int caseSwitch = Random.Range (0,3);
		switch (caseSwitch)
		{
		case 1:
			return GameObject.CreatePrimitive(PrimitiveType.Cube);
			break;
		case 2:
			return GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			break;
		case 3:
			return GameObject.CreatePrimitive(PrimitiveType.Sphere);
			break;
		default:
			return GameObject.CreatePrimitive(PrimitiveType.Plane);
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
