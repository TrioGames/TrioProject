﻿using UnityEngine;
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
			float xSpeed = GetComponent<Rigidbody>().velocity.x;
			float ySpeed = Mathf.Abs(GetComponent<Rigidbody>().velocity.y);
			//float ySpeed = rigidbody.velocity.y;
			float cons = maxSpeed / Mathf.Sqrt( (xSpeed * xSpeed) + (ySpeed * ySpeed) );
			//rigidbody.velocity = new Vector3(xSpeed * cons, (ySpeed * cons) + 5, rigidbody.velocity.z);
			GetComponent<Rigidbody>().velocity = new Vector3(xSpeed * cons, (ySpeed * cons), GetComponent<Rigidbody>().velocity.z);

			JumpSound.Play();
			count++;
		}

        if (col.gameObject.tag.Equals(Constants.TAG_PLATFORM) && Gamer.instance.ballMode != Constants.FIREBALL_MODE)
        {
            // DeathSound.Play();
            //DontDestroyOnLoad(DeathSound);
            print("ok");
            Score.instance.StoreHighScore();
            // col.gameObject.GetComponent<Collider>().isTrigger = true;
            StartCoroutine(WaitRestart());
        }

        if (col.gameObject.name == "RespawnPlane")
		{
			//DeathSound.Play();
            //DontDestroyOnLoad(DeathSound);
            Score.instance.StoreHighScore();
            // col.gameObject.GetComponent<Collider>().isTrigger = true;
            StartCoroutine(WaitRestart());
        }
	}

	IEnumerator WaitRestart()
	{
        //Do whatever you need done here before waiting
        //Gamer.instance.PauseGame();
        Gamer.instance.SlowmoGame();
        yield return new WaitForSeconds (0.2f);
        Application.LoadLevel("MenuScene");
        //do stuff after the 2 seconds
    }
	

	void OnTriggerEnter(Collider col)
	{
       
		if (col.gameObject.tag.Equals (Constants.TAG_BONUS))
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
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, Constants.SUPERBALL_SPEED_CONST , GetComponent<Rigidbody>().velocity.z); 
		}
		else if (col.gameObject.name.Equals(Constants.GRAVITY_BONUS))
		{
			Gamer.instance.EnableLowGravityMode ();
		}
	}

}
