using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour {
	public AudioSource ses1;
	public GUIText CountText;
	public int max_height = 0;
	private int count = 0;
	public float maxSpeed = 8;
	public Text HighScore;
	public TextMesh tm;
	// Use this for initialization

	public static int score;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//highscore count
		float curHeight = gameObject.transform.position.y;
		if (curHeight > max_height) {
			max_height = (int) curHeight / 10;
		}

		//booster count
		if (Gamer.instance.boostTimer > 0)
			CountText.text = ((int) Gamer.instance.boostTimer).ToString();
		else 
			CountText.text = "";
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
			//PointText.text = max_height;
			//CountText.text = max_height;
		}

		if (col.gameObject.name == "RespawnPlane")
		{
			Score.instance.StoreHighScore();
			Application.LoadLevel ("MenuScene");
		}
	}
	

	void OnTriggerEnter(Collider col)
	{
		//test print
		if (col.gameObject.tag.Equals (Constants.TAG_BONUS)) {
			//print ("BONUS: " + col.gameObject.name);
		}
			
		if (col.gameObject.name.Equals(Constants.FIREBALL_BONUS)) {
			Gamer.instance.EnableFireballMode (Constants.FIREBALL_TIMER_INIT);
		}
		else if (col.gameObject.name.Equals(Constants.SUPERBALL_BONUS))
		{
			Gamer.instance.EnableFireballMode (Constants.SUPERBALL_TIMER_INIT);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Constants.SUPERBALL_SPEED_CONST , rigidbody.velocity.z); 
		}
		else if (col.gameObject.name.Equals(Constants.GRAVITY_BONUS))
		{
			Gamer.instance.EnableLowGravityMode ();
		}
	}

}
