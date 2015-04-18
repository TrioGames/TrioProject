using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private Transform playerTrans;
	
	// Use this for initialization
	void Start () 
	{
		//player = GameObject.FindGameObjectWithTag ("Player");
		//offset = transform.position;
		
	}

	void Awake () {
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		//transform.position.y = BallBehaviour.max_height - 10;	
		float playerHeight = playerTrans.position.y;
		/*
		if (playerHeight > nextPlatformCheck)
		{
			PlatformMaintenaince(); //Spawn new platforms
		}*/
		
		//Update camera position if the player has climbed and if the player is too low: Set gameover.
		float currentCameraHeight = transform.position.y;
		float newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
		if (playerTrans.position.y > currentCameraHeight)
		{
			transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
		}else{
			//Player is lower..maybe below the cameras view?
			if (playerHeight < (currentCameraHeight - 10))
			{
				//GameOver();
			}
		}
	}
}
