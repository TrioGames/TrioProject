using UnityEngine;
using System.Collections;

public class Gamer : MonoBehaviour {
	

	
	private Transform playerTrans;
	private Transform planeTrans;
	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;
	public Transform platWarning;
	private Vector3 objInitPos = new Vector3 (0.0f, -10.0f , 0.0f);
	private Vector3 obj1Pos = new Vector3 (-0.6f, -0.48f , -17.2f);
	private Vector3 obj2Pos = new Vector3 (-0.035f , -0.48f, -17.2f);
	private Vector3 obj3Pos = new Vector3 (0.584f , -0.48f, -17.2f);
	public float objScale = 0.75f;
	private int defaultLayer = 0;
	private int voidLayer;
	public int GameStatus = Constants.GAME_STATUS_PAUSE;
	
	//prefabs
	public Transform platformPrefab;
	public Transform powerupPrefab;
	public GameObject starPrefab;
	public GameObject concavePrefab;
	public GameObject prismPrefab;
	public GameObject Teapotprefab;
	public GameObject Pyramidprefab;

	//platform variables
//	private float platformsSpawnedUpTo = 0.0f;
//	private float nextPlatformCheck = 2.0f;
	private ArrayList platforms;
	public float platformLower = Constants.HEIGHT_NO_PLATFORM;
	public float platformRange = 500.0f;

	//powerup variables
	public int ballMode = Constants.NORMAL_MODE;
	private float powerupSpawnedUpTo = 0.0f;
	private float nextPowerUpCheck = 5.0f;

	//public GUIText PlatformWarning;
	public float boostTimer = -1;
	//public float gravityTimer = -1;

	public bool LowGravityMode;

	// top yukseldikce platformlar daha sik geliyor
	private readonly float[,] spawnHeightArray  = new float[3,2] { {8.0f, 20.0f}, {5.0f, 12f}, {4.0f,8.0f} };
	private int gameLevel; //0-1-2 olabilir


	//for singleton
	public static Gamer instance { get; private set; }

	// Use this for initialization
	void Start () {
		PauseGame ();

		DisableLowGravityMode ();

		Score.instance.Start();

		Score.instance.ResetHighScore (5);

		gameLevel = 0;

		obj1 = GetRandomObject ();
		obj1.transform.position = obj1Pos;

		obj2 = GetRandomObject ();
		obj2.transform.position = obj2Pos;

		obj3 = GetRandomObject ();
		obj3.transform.position = obj3Pos;


	}

	void DisableLowGravityMode()
	{
		LowGravityMode = false;
		ballMode = Constants.NORMAL_MODE;
		Physics.gravity = new Vector3 (0, Constants.GRAVITY_DEFAULT, 0);
		boostTimer = -1;
	}

	public void EnableLowGravityMode()
	{
		LowGravityMode = true;
		ballMode = Constants.GRAVITY_MODE;
		Physics.gravity = new Vector3 (0, Constants.GRAVITY_LOW, 0);
		boostTimer = Constants.GRAVITY_TIMER;
	}

	void Awake () {
		voidLayer = LayerMask.NameToLayer( "Void" );
		instance = this;
		playerTrans = GameObject.FindGameObjectWithTag(Constants.TAG_BALL).transform;
		platforms = new ArrayList();
		//SpawnPlatforms(2.0f);
		StartGame();
	}

	
	public void StartGame()
	{
		GameStatus = Constants.GAME_STATUS_RUN;
		Time.timeScale = 1.0f;
	}

	public void PauseGame()
	{
		GameStatus = Constants.GAME_STATUS_PAUSE;
		mainMenuButtons.instance.UpdateScoreBoard();
		Time.timeScale = 0.0f;
	}

	public void EndGame()
	{
		GameStatus = Constants.GAME_STATUS_END;
		mainMenuButtons.instance.UpdateScoreBoard();
		Time.timeScale = 0.0f;
	}

	GameObject GetRandomObject()
	{
		int caseSwitch = Random.Range (0,5);
		GameObject obj;
		switch (caseSwitch)
		{
		case 0:
			obj = Instantiate(starPrefab, objInitPos, Quaternion.identity) as GameObject;
			//obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			obj.name = "Star";
			break;
		case 1:
			obj = Instantiate(concavePrefab, objInitPos, Quaternion.identity) as GameObject;
			//obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			obj.name = "Concave";
			break;
		case 2:
			obj = Instantiate(prismPrefab, objInitPos, Quaternion.identity) as GameObject;
			//obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.name = "Prism";
			break;
		case 3:
			obj = Instantiate(Teapotprefab, objInitPos, Quaternion.identity) as GameObject;
			//obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.name = "Teapot";
			break;
		case 4:
			obj = Instantiate(Pyramidprefab, objInitPos, Quaternion.identity) as GameObject;
			//obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.name = "Pyramid";
			break;
		default:
			obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
			obj.name = "Plane";
			break;
		}
		int randomAngle = (int) Random.Range(0, 8) * 45;
		var rot = transform.rotation;
		obj.transform.rotation = rot * Quaternion.Euler(0, 0, randomAngle); 
		obj.tag = "TrioObject";
		obj.AddComponent("mouseDrag");
		obj.AddComponent ("ObjectBehavior");
		obj.transform.localScale -= new Vector3(objScale, objScale, objScale);
		return obj;
	}

	public void RecreateMissingObject()
	{
		if (obj1 == null || obj1.name.StartsWith("Destroyed")) {
			//print ("obj1 is null. So recreating...");
			obj1 = GetRandomObject ();
			obj1.transform.position = obj1Pos;
			//print ("obj1 is : " + obj1.name );
		}

		if (obj2 == null || obj2.name.StartsWith("Destroyed")) {
			//print ("obj2 is null. So recreating...");
			obj2 = GetRandomObject ();
			obj2.transform.position = obj2Pos;
			//print ("obj2 is : " + obj2.name );
		}

		if (obj3 == null || obj3.name.StartsWith("Destroyed")) {
			//print ("obj3 is null. So recreating...");
			obj3 = GetRandomObject ();
			obj3.transform.position = obj3Pos;
			//print ("obj3 is : " + obj3.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		playerTrans = GameObject.FindGameObjectWithTag(Constants.TAG_BALL).transform;
		float playerHeight = playerTrans.position.y;
		RecreateMissingObject ();
		IncreaseGameDifficulty (playerHeight);
		MaintainPlatforms (playerHeight);
		MaintainPowerups (playerHeight);
		SpawnPowerups (playerHeight);
		Warn4ComingPlatforms (playerHeight);
	}

	private void IncreaseGameDifficulty(float playerHeight)
	{
		if (playerHeight > 60)
			gameLevel = 2;
		else if (playerHeight > 30)
			gameLevel = 1;
		else 
			gameLevel = 0;
	}

	private void Warn4ComingPlatforms(float playerHeight)
	{
		Transform plat =  GetNearestPlatformsCoordinates (playerHeight);
		if (plat != null) {
			//PlatformWarning.transform.position = new Vector3(plat.position.x , PlatformWarning.transform.position.y , PlatformWarning.transform.position.z );
			//PlatformWarning.enabled = true;
			Vector3 newPos = new Vector3(plat.position.x , platWarning.position.y , platWarning.position.z );
			platWarning.position = Vector3.MoveTowards(platWarning.position , newPos, 5 * Time.deltaTime);
			//print ((int)PlatformWarning.transform.position.x + "," + (int)PlatformWarning.transform.position.y);
		}
		else 
		{
			//PlatformWarning.enabled = false;	
		}
	}

	private Transform GetNearestPlatformsCoordinates (float warningHeightLimit)
	{
		string s = "Platforms: ";
		float nearestplat = warningHeightLimit + 1000;
		int nearestIndex = 0;
		bool platformsNearby = false;
		for (int i = platforms.Count-1; i>=0; i--) 
		{
			Transform plat = (Transform)platforms[i];
			s += "(" + plat.position.x.ToString("0.0")  + "," + plat.position.y.ToString("0.0") + "),";
			if (plat.position.y > warningHeightLimit)
			{
				if ( plat.position.y < nearestplat)
				{
					nearestplat = plat.position.y;
					nearestIndex = i;
					platformsNearby = true;
				}
			}
		}
		//return (Transform) platforms [nearestIndex];
		if (platformsNearby)
		{
			//print(s);
			//print ("Nearest : " + "(" + ((Transform) platforms[nearestIndex]).position.x.ToString("0.0") + "," +  ((Transform)platforms[nearestIndex]).position.y.ToString("0.0")  + ")");
			return (Transform) platforms [nearestIndex];
		}
		else 
			return null;
	}

	private void MaintainPowerups(float playerHeight)
	{
		// Update timer of the boost (fireball etc.)
		if (ballMode == Constants.FIREBALL_MODE) {
			boostTimer -= Time.deltaTime;
			if (boostTimer < 0)
			{
				ballMode = Constants.NORMAL_MODE;
				EnablePlatformsColliders();
			}
		}
		else if (ballMode == Constants.GRAVITY_MODE)
		{
			boostTimer -= Time.deltaTime;
			if (boostTimer < 0)
			{
				DisableLowGravityMode();
			}
		}
	}

	private void MaintainPlatforms(float playerHeight)
	{
		DeletePlatformsBelowPlane ();
		if (playerHeight > platformLower - platformRange / 2) 
		{
			SpawnPlatforms(platformLower, platformLower + platformRange );
		}
	}

	void SpawnPlatforms(float downTo, float upTo)
	{
		float spawnLowerLimit = 4.0f;
		float spawnHigherLimit = 18.0f;
		float spawnHeight = downTo;
		while (spawnHeight <= upTo) 
		{
			spawnLowerLimit = spawnHeightArray[gameLevel, 0];
			spawnHigherLimit = spawnHeightArray[gameLevel, 1];
			spawnHeight += Random.Range(spawnLowerLimit, spawnHigherLimit);
			float x = Random.Range(-0.8f, 0.8f);
			Vector3 pos = new Vector3(x, spawnHeight, -17.0f);
			
			Transform plat = (Transform) Instantiate(platformPrefab, pos, Quaternion.identity) ;
			plat.tag = "Platform";
			
			if (ballMode == Constants.NORMAL_MODE){ // normal mode
				plat.gameObject.GetComponent<Collider>().isTrigger = false;
			}
			else if (ballMode == Constants.FIREBALL_MODE){ // fireball
				plat.gameObject.GetComponent<Collider>().isTrigger = true;
			}
			
			platforms.Add(plat);
		}
		platformLower = spawnHeight;
	}

//	void SpawnPlatforms(float upTo)
//	{
//		float spawnHeight = platformsSpawnedUpTo;
//		while (spawnHeight <= upTo)
//		{
//			spawnHeight += Random.Range(5.0f, 20.0f);
//			float x = Random.Range(-0.8f, 0.8f);
//			Vector3 pos = new Vector3(x, spawnHeight, -17.0f);
//			
//			Transform plat = (Transform) Instantiate(platformPrefab, pos, Quaternion.identity) ;
//			plat.tag = "Platform";
//			
//			if (ballMode == Constants.NORMAL_MODE){ // normal mode
//				plat.gameObject.GetComponent<Collider>().isTrigger = false;
//			}
//			else if (ballMode == Constants.FIREBALL_MODE){ // fireball
//				plat.gameObject.GetComponent<Collider>().isTrigger = true;
//			}
//			
//			platforms.Add(plat);
//
//		}
//		platformsSpawnedUpTo = spawnHeight;
//
//	}

	public void EnableFireballMode(float timer)
	{
		DisablePlatformsColliders ();
		ballMode = Constants.FIREBALL_MODE;
		boostTimer = timer;
	}
	
	public void SpawnPowerups(float playerHeight)
	{
		if (playerHeight > powerupSpawnedUpTo + nextPowerUpCheck) 
		{		
			float y = playerHeight + Random.Range(5.6f, 9.5f);
			float x = Random.Range(-0.8f, 0.8f);
			Vector3 pos = new Vector3(x, y, -17.0f);
			
			Transform powerup = (Transform) Instantiate(powerupPrefab, pos, Quaternion.identity) ;
			powerup.name = GetBonus();
			if (powerup.name.Equals(Constants.FIREBALL_BONUS))
			{
				powerup.gameObject.renderer.material.color = Color.red;
			}
			else if (powerup.name.Equals(Constants.SUPERBALL_BONUS))
			{
				powerup.gameObject.renderer.material.color = Color.blue;
			}
			else if (powerup.name.Equals(Constants.GRAVITY_BONUS))
			{
				powerup.gameObject.renderer.material.color = Color.green;
			}
			powerupSpawnedUpTo += y;
		}

	}

	private string GetBonus()
	{
		// %60 fireball, %20 superball powerup, %20 gravitiy
		int randBonus = (int)Random.Range (1, 11);
		if (randBonus < 7)
		{
			return Constants.FIREBALL_BONUS;
		}
		else if (randBonus < 9)
		{
			return Constants.SUPERBALL_BONUS;
		}
		else if (randBonus < 11)
		{
			return Constants.GRAVITY_BONUS;
		}
		else{
			return "";
		}
	}

	public void DisablePlatforms4BoostedBall()
	{
		for (int i = platforms.Count-1; i>=0; i--) {
			Transform plat = (Transform)platforms[i];
			print("Layer:" + plat.gameObject.layer);
			defaultLayer = plat.gameObject.layer;
			plat.gameObject.layer = voidLayer;
		}
	}

	public void DisablePlatformsColliders()
	{
		for (int i = platforms.Count-1; i>=0; i--) {
			Transform plat = (Transform)platforms[i];
			plat.gameObject.GetComponent<Collider>().isTrigger = true;
		}
	}

	public void EnablePlatformsColliders()
	{
		for (int i = platforms.Count-1; i>=0; i--) {
			Transform plat = (Transform)platforms[i];
			plat.gameObject.GetComponent<Collider>().isTrigger = false;
		}
	}

	public void EnablePlatforms4MainBall()
	{
		for (int i = platforms.Count-1; i>=0; i--) {
			Transform plat = (Transform)platforms[i];
			plat.gameObject.layer = defaultLayer;
		}
	}

	void DeletePlatformsBelowPlane()
	{
				for (int i = platforms.Count-1; i>=0; i--) 
				{
						Transform plat = (Transform)platforms [i];
						planeTrans = GameObject.FindGameObjectWithTag (Constants.TAG_PLANE).transform;
						if (plat == null)
						{
								platforms.RemoveAt (i);
						}
						else 
						{
								if (plat.position.y < planeTrans.position.y) 
								{					
										Destroy (plat.gameObject);
										platforms.RemoveAt (i);
								}     			
						}
				}
		}
}
