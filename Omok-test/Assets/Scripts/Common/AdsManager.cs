using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdsManager : Singleton<AdsManager>
{
    private string rewardAdUnitId = "ca-app-pub-3940256099942544/5224354917";
    
    private RewardedAd _rewardedAd;

    private void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            LoadRewardedAd();
        });
    }

    private void LoadRewardedAd()
    {
        if(_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        var adRequest = new AdRequest();
        
        RewardedAd.Load(rewardAdUnitId, adRequest, (rewardedAd, error) =>
        {
            if (error != null || rewardedAd == null)
            {
                Debug.Log("RewardedAd load failed");
                return;
            }
            
            _rewardedAd = rewardedAd;

        });
    }
    
    public void ShowRewardedAd(Action onUserEarnedReward, Action onAdClosed)
    {
        if (_rewardedAd == null)
        {
            Debug.Log("RewardedAd is not loaded");
            return;
        }

        _rewardedAd.Show(reward =>
        {
            onUserEarnedReward?.Invoke();
        });

        _rewardedAd.OnAdFullScreenContentClosed += () =>
        {
            onAdClosed?.Invoke();
            LoadRewardedAd();
        };
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
