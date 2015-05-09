using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour {
	public AudioSource ses1;
	public GUIText CountText;
	public TextMesh PointText;
	public static float max_height = 0;
	private int count = 0;
	public float maxSpeed = 8;
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
			float xSpeed = rigidbody.velocity.x;
			float ySpeed = Mathf.Abs(rigidbody.velocity.y);
			float cons = maxSpeed / Mathf.Sqrt( (xSpeed * xSpeed) + (ySpeed * ySpeed) );
			rigidbody.velocity = new Vector3(xSpeed * cons, (ySpeed * cons) + 5 , rigidbody.velocity.z); 
			//rigidbody.velocity = new Vector3(xSpeed , 10, rigidbody.velocity.z); 
			ses1.Play();
			count++;
			PointText.text = count.ToString();
			CountText.text = "Points: " + count.ToString();
		}

		if (col.gameObject.name == "RespawnPlane")
		{
			//print("Game Over");
			Application.LoadLevel (0);
		}
	}

}
