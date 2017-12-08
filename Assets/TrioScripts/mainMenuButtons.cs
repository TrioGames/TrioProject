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
        FacebookManager.Instance.InitFB();
    }

    public void UpdateScoreBoard()
    {
        if (null != Skor)
            Skor.text = "HIGHSCORE: " + Score.instance.GetHighScore().ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        Gamer.instance.PauseGame();
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
        }
    }

    public void DisplayUserName(string result)
    {
        Text UserName = HighScoreText.GetComponent<Text>();
        UserName.text = result + ": " + Score.instance.GetHighScore().ToString();
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
        Debug.Log("Login Clicked");
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
    }
}

