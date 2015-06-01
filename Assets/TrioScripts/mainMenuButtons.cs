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
		Gamer.instance.PauseGame ();
	}

	public void ExitGame () {
		//Application.Quit ();
		Application.LoadLevel ("MenuScene");
	}

	public void PauseGame () {
		Gamer.instance.PauseGame ();
		menu.SetActive(true);
	}

	public void ResumeGame () {
		Gamer.instance.StartGame ();
		menu.SetActive(false);
	}
}
