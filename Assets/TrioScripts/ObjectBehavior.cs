using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {

	//private int collisionCount = 0;

	private Gamer gamer = new Gamer();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		//collisionCount += 1;
		//if (collisionCount == 2) {
		gameObject.name = "Destroyed" + gameObject.name;
		print ("Destroying " + gameObject.name);
		DestroyObject (gameObject);	
		//}
	}
	
}
