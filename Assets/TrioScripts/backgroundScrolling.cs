using UnityEngine;
using System.Collections;

public class backgroundScrolling : MonoBehaviour {

	public float Speed;

	public TextMesh ScoreMesh;
	
	float height;
	
	void Start()
	{

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
				ScoreMesh.text = Score.instance.Count.ToString();
		}
	}
}