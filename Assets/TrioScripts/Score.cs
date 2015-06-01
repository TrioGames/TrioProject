using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public static Score instance = new Score();

	public Score GetInstance()
	{
		if (instance == null)
			instance = new Score();
		return instance;
	}

	public int Count;
	
	public void StoreHighScore()
	{
		int oldHighScore = PlayerPrefs.GetInt ("highscore", 0);
		print ("OldScore : " + oldHighScore);
		if (Count > oldHighScore) {
			print ("22222");
			PlayerPrefs.SetInt ("highscore", Count);
			PlayerPrefs.Save ();
			print ("NewHighScore : " + Count);
		}
	}

	public int GetHighScore()
	{
		return PlayerPrefs.GetInt ("highscore", 0);
	}

	// Use this for initialization
	void Start () {
		Count = 0;
	}


	// Update is called once per frame
	void Update () {
	
	}
}
