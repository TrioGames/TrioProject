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
		if (Count -1 > oldHighScore) {
			PlayerPrefs.SetInt ("highscore", Count - 1);
			PlayerPrefs.Save ();
		}
	}

	public void ResetHighScore(int i)
	{
		PlayerPrefs.SetInt ("highscore", i);
		PlayerPrefs.Save ();
	}

	public int GetHighScore()
	{
		return PlayerPrefs.GetInt ("highscore", 0);
	}

	// Use this for initialization
	public void Start () {
		Count = 0;
	}


	// Update is called once per frame
	void Update () {
	
	}
}
