using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class backgroundScrolling : MonoBehaviour {

	public float Speedasd;

	public TextMesh ScoreMesh;

	public TextMesh NewHighscoreMsg;
	
	int oldScore;
	
	void Start()
	{
		oldScore = Score.instance.GetHighScore();
	}
	// Update is called once per frame
	void Update () 
	{
		Camera cam = Camera.main;
		float camHeight = cam.transform.position.y;
		if (camHeight > transform.position.y + 6.0f) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 12f, transform.position.z);
			Score.instance.Count++;
			if (Score.instance.Count != 0)
			{
				ScoreMesh.text = Score.instance.Count.ToString();
				if (Score.instance.Count == oldScore + 1)
				{
					NewHighscoreMsg.text = "New Record!";
				}
				else if(Score.instance.Count > oldScore + 1)
				{
					NewHighscoreMsg.text = "";
				}
			}
		}
	}
}