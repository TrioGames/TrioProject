using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using Facebook.MiniJSON;

public class mainMenuButtons : MonoBehaviour
{
    public GameObject menu;
    public GameObject resumeText;
    public GameObject pauseButton;
    public GameObject perde;
    public GameObject CreditsMenu;
    public GameObject NewGameButton;
    public GameObject ExitButton;
    public GameObject LoginButton;
    public GameObject LogoutButton;
    public GameObject HighScoreText;
    public GameObject Avatar;
    public GameObject ScoreMenu;
    public GameObject ScoreView;
    public GameObject ScoreBoard;
    public GameObject FriendScorePanel;
    public static mainMenuButtons instance { get; private set; }

    public Text Skor;
    // Use this for initialization
    void Start()
    {
        UpdateScoreBoard();
        LogButtonShowHide();
    }

    void Awake()
    {
        instance = this;
        FacebookManager.Instance.InitFB();
        //ScoreView.SetActive(false);
    }

    public void UpdateScoreBoard()
    {
        if (null != Skor)
            DisplayUserName("Guesst");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");

        if (Gamer.instance != null)
        {
            Gamer.instance.PauseGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        SceneManager.LoadScene("MenuScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void DisplayCredits()
    {
        menu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void PauseGame()
    {
        Gamer.instance.PauseGame();
        float newScore = perde.transform.position.y + 1.71f;
        resumeText.transform.position = new Vector3(resumeText.transform.position.x, newScore, resumeText.transform.position.z);
        resumeText.SetActive(true);
        pauseButton.SetActive(false);
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

    public void LogButtonShowHide()
    {
        var isLoggedIn = FacebookManager.Instance.IsLoggedIn;

        if (!isLoggedIn && LogoutButton != null)
        {
            LogoutButton.SetActive(false);
            LoginButton.SetActive(true);
            Avatar.SetActive(false);

            DisplayUserName("Guesst");
            DestroyScoreBoardPanels();

        }
        else if (LogoutButton != null)
        {
            LogoutButton.SetActive(true);
            LoginButton.SetActive(false);
        }
    }

    public void DisplayUserName(string result)
    {
        if (HighScoreText != null)
        {
            Text UserName = HighScoreText.GetComponent<Text>();
            UserName.text = result + ": " + Score.instance.GetHighScore().ToString();
        }
    }

    public void DisplayAvatar(IGraphResult result)
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

    public void Login()
    {
        FacebookManager.Instance.FBLogin(delegate (ILoginResult res)
        {
            if (res.Error != null)
            {
                Debug.Log("Login Failed!!!");
            }
            else
            {
                CreateProfile();
            }
        });
    }

    public void Logout()
    {
        FacebookManager.Instance.FBLogout();
        LogButtonShowHide();
    }

    public void DestroyScoreBoardPanels()
    {
        foreach (Transform child in ScoreBoard.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void DisplayScoreView()
    {
        ScoreMenu.SetActive(true);
        menu.SetActive(false);
    }

    public void CreateProfile()
    {
        FacebookManager.Instance.GetFBName(delegate (IGraphResult _result)
        {
            if (_result.Error != null)
            {
                DisplayUserName("Guesst");
            }
            else
            {
                DisplayUserName(_result.ResultDictionary["first_name"].ToString());
            }
        });

        FacebookManager.Instance.GetFBAvatar(delegate (IGraphResult _result)
        {
            if (_result.Error != null)
            {
                Avatar.SetActive(false);
            }
            else
            {
                DisplayAvatar(_result);
            }
        });

        FacebookManager.Instance.GetFBScore(delegate (IGraphResult _result)
        {
            if (_result.Error != null)
            {
                Text UserName = HighScoreText.GetComponent<Text>();
                UserName.text = "HIGH SCORE: 0";
            }
            else
            {
                Text UserName = HighScoreText.GetComponent<Text>();
                UserName.text = "HIGH SCORE: " + _result.ResultDictionary["score"].ToString();
            }
        });

        FacebookManager.Instance.GetFBFriendList(delegate (IGraphResult _result)
        {
            if (_result.Error != null)
            {

            }
            else
            {
                var dict = Json.Deserialize(_result.RawResult) as Dictionary<string, object>;

                var friendList = new List<object>();
                friendList = (List<object>)(dict["data"]);

                DestroyScoreBoardPanels();

                foreach (object friend in friendList)
                {
                    var entry = (Dictionary<string, object>)friend;
                    var user = (Dictionary<string, object>)entry["user"];

                    GameObject ScorePanel;
                    ScorePanel = Instantiate(FriendScorePanel) as GameObject;
                    ScorePanel.transform.SetParent(ScoreBoard.transform, false);

                    Transform ThisScoreName = ScorePanel.transform.Find("FriendName");
                    Transform ThisScoreScore = ScorePanel.transform.Find("FriendScore");

                    Text ScoreName = ThisScoreName.GetComponent<Text>();
                    Text ScoreScore = ThisScoreScore.GetComponent<Text>();

                    ScoreName.text = user["name"].ToString();
                    ScoreScore.text = entry["score"].ToString();

                    Transform ThisUserAvatar = ScorePanel.transform.Find("FriendAvatar");
                    Image UserAvatar = ThisUserAvatar.GetComponent<Image>();

                    FB.API("/" + user["id"].ToString() + "/picture?type=square&height=128&width=128", HttpMethod.GET, delegate (IGraphResult result)
                    {
                        if (result.Error != null)
                        {
                            Debug.Log(result.Error);
                            return;
                        }

                        UserAvatar.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
                    });
                }
            }
        });

        LogButtonShowHide();
    }
}

