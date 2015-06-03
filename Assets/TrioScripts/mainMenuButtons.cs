using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainMenuButtons : MonoBehaviour {
	public GameObject menu;
	public GameObject resumeText;
	public GameObject pauseButton;
	public GameObject perde;
	public GameObject CreditsMenu;
	public static mainMenuButtons instance { get; private set; }

	public Text Skor;
	// Use this for initialization
	void Start () {
		UpdateScoreBoard ();
	}

	void Awake(){
		instance = this;
	}

	public void UpdateScoreBoard()
	{
		if (null != Skor)
			Skor.text = "HIGHSCORE: " + Score.instance.GetHighScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame () {
		Application.LoadLevel ("GameScene");
		Gamer.instance.PauseGame ();
	}

	public void ExitGame () {
		Application.Quit ();
		Application.LoadLevel ("MenuScene");
	}

	public void DisplayCredits () {
		menu.SetActive (false);
		CreditsMenu.SetActive (true);
	}

	public void BackToMenu()
	{
//		CreditsMenu.SetActive (false);
//		menu.SetActive (true);
		Application.LoadLevel ("MenuScene");
	}

	public void PauseGame () {
		Gamer.instance.PauseGame ();
		float newScore = perde.transform.position.y + 1.71f;
		resumeText.transform.position = new Vector3 (resumeText.transform.position.x, newScore, resumeText.transform.position.z);
		resumeText.SetActive (true);
		pauseButton.SetActive (false);
		//menu.SetActive(true);
	}
	public void ActivatePauseButton() {
		pauseButton.SetActive (true);
	}

	public void ResumeGame () {
		pauseButton.SetActive (true);
		menu.SetActive(false);
		Gamer.instance.StartGame ();
	}
}
