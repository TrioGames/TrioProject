using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;



public class mainMenuButtons : MonoBehaviour
{
    public GameObject menu;
    public GameObject resumeText;
    public GameObject pauseButton;
    public GameObject perde;
    public GameObject CreditsMenu;
    public GameObject LoginButton;
    public GameObject LogoutButton;
    public GameObject HighScoreText;
    public GameObject Avatar;
    public static mainMenuButtons instance { get; private set; }

    private List<object> scoresList = null;

    public Text Skor;
    // Use this for initialization
    void Start()
    {
        UpdateScoreBoard();
    }

    void Awake()
    {
        instance = this;

        FB.Init(SetInit, OnHideUnity);
    }

    private void OnHideUnity(bool isUnityShown)
    {
        if (!isUnityShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void UpdateScoreBoard()
    {
        if (null != Skor)
            Skor.text = "HIGHSCORE: " + Score.instance.GetHighScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Application.LoadLevel("GameScene");
        Gamer.instance.PauseGame();
    }

    public void ExitGame()
    {
        Application.Quit();
        Application.LoadLevel("MenuScene");
    }

    public void DisplayCredits()
    {
        menu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        //		CreditsMenu.SetActive (false);
        //		menu.SetActive (true);
        Application.LoadLevel("MenuScene");
    }

    public void PauseGame()
    {
        Gamer.instance.PauseGame();
        float newScore = perde.transform.position.y + 1.71f;
        resumeText.transform.position = new Vector3(resumeText.transform.position.x, newScore, resumeText.transform.position.z);
        resumeText.SetActive(true);
        pauseButton.SetActive(false);
        //menu.SetActive(true);
    }
    public void ActivatePauseButton()
    {
        pauseButton.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        menu.SetActive(false);
        Gamer.instance.StartGame();
    }


    // ******************** Facebook Connections ******************** \\

    void SetInit()
    {
        LogButtonShowHide();
    }

    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    void AuthCallback(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }

        LogButtonShowHide();
    }

    public void FBLogout()
    {
        FB.LogOut();
        LogButtonShowHide();
        Score.instance.ResetHighScore(0);
    }

    private void LogButtonShowHide()
    {
        var isLoggedIn = FB.IsLoggedIn;

        if (!isLoggedIn)
        {
            LogoutButton.SetActive(false);
            LoginButton.SetActive(true);

            Text UserName = HighScoreText.GetComponent<Text>();
            UserName.text = "HIGH SCORE: " + Score.instance.GetHighScore().ToString();

            Avatar.SetActive(false);
        }
        else
        {
            LogoutButton.SetActive(true);
            LoginButton.SetActive(false);

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUserName);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayAvatar);
        }
    }

    void DisplayUserName(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
            return;
        }

        Text UserName = HighScoreText.GetComponent<Text>();
        UserName.text = result.ResultDictionary["first_name"].ToString() + ": " + Score.instance.GetHighScore().ToString();

        SetScore();
        
    }

    void DisplayAvatar(IGraphResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
            return;
        }

        if (result.Texture == null)
        {
            return;
        }

        Avatar.SetActive(true);
        Image avatar = Avatar.GetComponent<Image>();
        avatar.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
    }

    void UserListCallback(IResult result)
    {
        Debug.Log(result.RawResult);
    }

    void SetScore()
    {
        var scoreData = new Dictionary<string, string>();

        scoreData["score"] = Score.instance.GetHighScore().ToString();

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result) {
            Debug.Log("score submit result: " + result.RawResult);

            FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, UserListCallback);
        }, scoreData);
    }
}

