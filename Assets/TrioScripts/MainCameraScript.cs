using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {
	
	private Transform playerTrans;

	private float initCameraHeight;

	private float currentCameraHeight;

	public static float CameraInc = 0;
	
	// Use this for initialization
	void Start () 
	{
		initCameraHeight = transform.position.y;
	}

	void Awake () {
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		float playerHeight = playerTrans.position.y;
		float currentCameraHeight = transform.position.y;
		float newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
		if (playerTrans.position.y > currentCameraHeight)
		{
			currentCameraHeight = newHeight;
			transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
			CameraInc = currentCameraHeight - initCameraHeight;
		}
	}
}
