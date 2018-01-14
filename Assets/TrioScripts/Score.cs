using Facebook.Unity;
using UnityEngine;

public class Score : MonoBehaviour
{

    public static Score instance = new Score();

    public Score GetInstance()
    {
        if (instance == null)
            instance = new Score();
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
    public int Count;

    public void StoreHighScore()
    {
        int oldHighScore = PlayerPrefs.GetInt("highscore", 0);
        if (Count - 1 > oldHighScore)
        {
            SetHighScore(null);
        }
    }

    public void SetHighScore(int? score)
    {
        if (score != null)
        {
            SetFBScore(score.ToString());
        } else
        {
            PlayerPrefs.SetInt("highscore", Count - 1);
            PlayerPrefs.Save();
            SetFBScore((Count - 1).ToString());
        }
    }

    public void ResetHighScore(int i)
    {
        PlayerPrefs.SetInt("highscore", i);
        PlayerPrefs.Save();
        
    }

    public void SetFBScore(string score)
    {
        if (FB.IsLoggedIn)
        {
            FacebookManager.Instance.PostFBScore(score.ToString(), delegate (IGraphResult result)
            {
                if (result.Error != null)
                {
                    Debug.Log("error setting high score!");
                    return;
                }
                Debug.Log("success setting high score!");
            });
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    // Use this for initialization
    public void Start()
    {
        Count = 0;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
