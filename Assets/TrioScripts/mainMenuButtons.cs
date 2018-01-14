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
    public GameObject UserName;
    public GameObject Avatar;
    public GameObject GameMenu;
    public GameObject MainMenu;
    public GameObject HighScoreMenu;
    public GameObject ScoreMenu;
    public GameObject ScoreView;
    public GameObject ScoreBoard;
    public GameObject FriendScorePanel;

    private string FBName;
    private IGraphResult FBAvatar;


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
        if (HighScoreMenu != null)
        {
            HighScoreMenu.SetActive(false);
        }
        UpdateScoreBoard();
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
    }

    public void BackToMenu()
    {
        MainMenu.SetActive(true);
        HighScoreMenu.SetActive(false);
        CreditsMenu.SetActive(false);
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
        else if (isLoggedIn && LogoutButton != null)
        {
            CreateUserInfo();
            //LogoutButton.SetActive(true);
            LoginButton.SetActive(false);
        }
    }
    public void CreateUserInfo()
    {
        FacebookManager.Instance.GetFBName(delegate (IGraphResult _result)
        {
            if (_result.Error != null)
            {
                DisplayUserName("Guesst");
            }
            else
            {
                FBName = _result.ResultDictionary["first_name"].ToString();
                DisplayUserName(FBName);
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
                FBAvatar = _result;
                DisplayAvatar(_result);
            }
        });
    }

    public void DisplayUserName(string result)
    {
        SetUserNameText(result);
        SetHighScoreText();
    }

    public void SetUserNameText(string result)
    {
        if (UserName != null)
        {
            Text _UserName = UserName.GetComponent<Text>();
            _UserName.text = result;
        }
    }

    public void SetHighScoreText()
    {
        if (HighScoreText != null)
        {
            Text _HighScoreText = HighScoreText.GetComponent<Text>();
            _HighScoreText.text = Score.instance.GetHighScore().ToString();
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
        if(FB.IsLoggedIn)
        {
            GetFriendList();
            HighScoreMenu.SetActive(true);
            MainMenu.SetActive(false);
        } else
        {
            Login();
        }
    }

    public void setscore()
    {
        FacebookManager.Instance.PostFBScore("1", delegate (IGraphResult result)
        {
            if (result.Error != null)
            {
                Debug.Log(result.Error.ToString());
                return;
            }
            Debug.Log("success setting high score!");
        });
    }

    public void getscore()
    {
        FacebookManager.Instance.GetFBScore(delegate (IGraphResult result)
        {
            if (result.Error != null)
            {
                Score.instance.ResetHighScore(0);
            }
            else
            {
                var dict = Json.Deserialize(result.RawResult) as Dictionary<string, object>;

                var scoreList = new List<object>();
                scoreList = (List<object>)(dict["data"]);
                object score = scoreList[0];
                var entry = (Dictionary<string, object>)score;
                string _score = entry["score"].ToString();
                Debug.Log(_score);
            }
        });
    }

    public void GetFriendList()
    {
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
                FBName = _result.ResultDictionary["first_name"].ToString();
                DisplayUserName(FBName);
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
                FBAvatar = _result;
                DisplayAvatar(_result);
            }
        });

        FacebookManager.Instance.GetFBScore(delegate (IGraphResult _result)
        {
            if (_result.Error != null)
            {
                Score.instance.ResetHighScore(0);
            }
            else
            {
                var dict = Json.Deserialize(_result.RawResult) as Dictionary<string, object>;
                var scoreList = new List<object>();
                scoreList = (List<object>)(dict["data"]);
                object score = scoreList[0];
                var entry = (Dictionary<string, object>)score;
                int FBScore = int.Parse(entry["score"].ToString());
                int localStore = Score.instance.GetHighScore();

                if (localStore > FBScore)
                {
                    Score.instance.SetHighScore(localStore);
                    SetHighScoreText();
                }
            }
        });

        LogButtonShowHide();
        FacebookManager.Instance.FBFeedShare();
    }
}

