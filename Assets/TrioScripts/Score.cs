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
		if (Count > oldHighScore) {
			PlayerPrefs.SetInt ("highscore", Count);
			PlayerPrefs.Save ();
			print (Count);
		}
	}

	// Use this for initialization
	void Start () {
		Count = 0;
	}


	// Update is called once per frame
	void Update () {
	
	}
}
