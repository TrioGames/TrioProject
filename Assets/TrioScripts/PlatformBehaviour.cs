using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour {

	public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (Gamer.instance.ballMode == Constants.FIREBALL_MODE)
		{
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			DestroyObject (gameObject);	
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (Gamer.instance.ballMode == Constants.FIREBALL_MODE)
		{
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			DestroyObject (gameObject);	
		}
	}
	
}
