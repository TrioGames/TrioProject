using UnityEngine;
using System.Collections;

public class PowerupBehaviour : MonoBehaviour {

	private Transform planeTrans;

	public float powerupTimer = 3;	

	// Use this for initialization
	void Start () {
		planeTrans = GameObject.FindGameObjectWithTag (Constants.TAG_PLANE).transform;
		powerupTimer = Constants.POWERUP_LIFE_TIMER;
	}
	
	// Update is called once per frame
	void Update () {
		powerupTimer -= Time.deltaTime;
		if (powerupTimer < 0)
			DestroyObject (gameObject);	
		planeTrans = GameObject.FindGameObjectWithTag (Constants.TAG_PLANE).transform;
		if (transform.position.y < planeTrans.position.y) 
		{					
			Destroy (gameObject);
		}     	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals (Constants.TAG_BALL)) {
			Destroy (gameObject);
		}
	}
}
