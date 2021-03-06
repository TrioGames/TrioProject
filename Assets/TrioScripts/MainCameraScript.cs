﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainCameraScript : MonoBehaviour {
	
	private Transform playerTrans;

	private float initCameraHeight;

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
        var activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "MenuScene")
        {
            return;
        }

        float playerHeight = playerTrans.position.y;
		float currentCameraHeight = transform.position.y;
		float newHeight;
		if (playerTrans.position.y > currentCameraHeight)
		{
			if (!Gamer.instance.isThereAnyTrioObject())
			{
				newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
				currentCameraHeight = newHeight;
				transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
				CameraInc = currentCameraHeight - initCameraHeight;
			}
			else{
				newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
				currentCameraHeight = newHeight;
				transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
				CameraInc = currentCameraHeight - initCameraHeight;
			}
		}

	}
}
