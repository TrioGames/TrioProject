using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour {
	public AudioSource ses1;
	public GUIText CountText;
	public static float max_height = 0;
	private int count = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y > max_height) {
			max_height = gameObject.transform.position.y;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		//if (col.gameObject.tag == "TrioObject" || col.gameObject.tag == "Platform") {
		if (col.gameObject.tag == "TrioObject") {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, rigidbody.velocity.z); 
			ses1.Play();
			count++;
			CountText.text = "Puan: " + count.ToString();

		}

		if (col.gameObject.name == "RespawnPlane")
		{
			print("Game Over");
			Application.LoadLevel (0);
		}
	}

}
