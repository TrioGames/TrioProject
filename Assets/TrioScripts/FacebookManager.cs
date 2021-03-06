﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour
{
    private static FacebookManager _instance;

    public static FacebookManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject fbm = new GameObject("FacebookManager");
                fbm.AddComponent<FacebookManager>();
            }

            return _instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public string UserName { get; set; }
    public Sprite Avatar { get; set; }
    public string HighScore { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
    }

    public void InitFB()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(AfterFBInit, OnHideUnity);
        }
        else
        {
            IsLoggedIn = FB.IsLoggedIn;
        }
    }

    void AfterFBInit()
    {
        //FBLogin(res => {
        //    if (res.Error != null)
        //    {
        //        Debug.Log("Login Failed!!!");
        //    }
        //    else
        //    {
        //        mainMenuButtons.instance.CreateProfile();
        //    }
        //});
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

    public void FBLogin(Action<ILoginResult> action)
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, delegate (ILoginResult result)
        {
            IsLoggedIn = FB.IsLoggedIn;
            action(result);
        });
    }

    public void FBLogout()
    {
        FB.LogOut();
        IsLoggedIn = FB.IsLoggedIn;
    }

    public void PostFBScore(string score, Action<IGraphResult> action)
    {
        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = score;

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result)
        {
            action(result);
        }, scoreData);
    }

    public void GetFBScore(Action<IGraphResult> action)
    {
        FB.API("/me/scores", HttpMethod.GET, delegate (IGraphResult result)
        {
            action(result);
        });
    }

    public void GetFBName(Action<IGraphResult> action)
    {
        FB.API("/me?fields=first_name", HttpMethod.GET, delegate (IGraphResult result)
        {
            action(result);
        });
    }

    public void GetFBFriendList(Action<IGraphResult> action)
    {
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, delegate (IGraphResult result)
        {
            action(result);
        });
    }

    public void GetFBAvatar(Action<IGraphResult> action)
    {
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, delegate (IGraphResult result)
        {
            action(result);
        });
    }

    public void FBInvite()
    {
        FB.Mobile.AppInvite(
            new Uri("AppLinkUrl"),
            new Uri("picture"),
            delegate (IAppInviteResult result)
        {

        });
    }

    public void AppRequestFBFriends()
    {
        FB.AppRequest(
            "Come play this great game!",
            null, null, null, null, null, null,
            delegate (IAppRequestResult result)
            {
                Debug.Log(result.RawResult);
            }
        );
    }

    public void FBFeedShare()
    {
        FB.FeedShare(
            linkCaption: "This is my highscore!!",
            picture: new Uri("https://www.loteriadecatalunya.cat/imageassets/game-trio.svg"),
            link: new Uri("http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.ClientToken : "guest")),
            linkName: "Trio",
            callback: delegate(IShareResult result)
        {

        });

    }
}
