using UnityEngine;
using System.Collections;

public class mainMenuButtons : MonoBehaviour {
	public GameObject menu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame () {
		Application.LoadLevel ("GameScene");
	}

	public void ExitGame () {
		//Application.Quit ();
		Application.LoadLevel ("MenuScene");
	}

	public void PauseGame () {
		Time.timeScale = 0;
		menu.SetActive(true);
	}

	public void ResumeGame () {
		Time.timeScale = 1;
		menu.SetActive(false);
	}
}
