using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour {
	public AudioSource JumpSound;
	public AudioSource DeathSound;
	public GUIText CountText;
	public int max_height = 0;
	private int count = 0;
	public float maxSpeed = 12;
	public Text HighScore;
	public TextMesh tm;
	// Use this for initialization

	public AudioSource PowerupSound;

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
			//float ySpeed = rigidbody.velocity.y;
			float cons = maxSpeed / Mathf.Sqrt( (xSpeed * xSpeed) + (ySpeed * ySpeed) );
			//rigidbody.velocity = new Vector3(xSpeed * cons, (ySpeed * cons) + 5, rigidbody.velocity.z);
			rigidbody.velocity = new Vector3(xSpeed * cons, (ySpeed * cons), rigidbody.velocity.z);

			JumpSound.Play();
			count++;
		}

		if (col.gameObject.name == "RespawnPlane")
		{
			DeathSound.Play();
			//DontDestroyOnLoad(DeathSound);
			Score.instance.StoreHighScore();
			Application.LoadLevel ("MenuScene");
		}
	}

	IEnumerator wait(float f)
	{
		//Do whatever you need done here before waiting
		
		yield return new WaitForSeconds (2f);
		
		//do stuff after the 2 seconds
	}
	

	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.tag.Equals (Constants.TAG_PLATFORM)) {
			JumpSound.Play();
		}
		else if (col.gameObject.tag.Equals (Constants.TAG_BONUS))
		{
			//PowerupSound.Play();
		}
			
		if (col.gameObject.name.Equals(Constants.FIREBALL_BONUS)) 
		{
			Gamer.instance.EnableFireballMode (Constants.FIREBALL_TIMER_INIT, Constants.FIREBALL_BONUS);
		}
		else if (col.gameObject.name.Equals(Constants.SUPERBALL_BONUS))
		{
			Gamer.instance.EnableFireballMode (Constants.SUPERBALL_TIMER_INIT, Constants.SUPERBALL_BONUS );
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, Constants.SUPERBALL_SPEED_CONST , rigidbody.velocity.z); 
		}
		else if (col.gameObject.name.Equals(Constants.GRAVITY_BONUS))
		{
			Gamer.instance.EnableLowGravityMode ();
		}
	}

}
