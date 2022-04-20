//using ByteDance.Union;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;
//using UnityEngine;

//public class RewardAdsCallbackClass : MonoBehaviour, IRewardVideoAdListener, IRewardAdInteractionListener, IRewardAdInteractionListener, IAppDownloadListener
//{
//    private static int mainThreadId;
//    private void Awake()
//    {
//        mainThreadId = Thread.CurrentThread.ManagedThreadId;
//    }
//    public void OnError(int code, string message)
//    {
//        Debug.LogError($"OnRewardError: {message}  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
//        if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
//            PangleAdsManager.Instance.information = "OnRewardError: " + message;
//    }

//    public void OnRewardVideoAdLoad(RewardVideoAd ad)
//    {
//#if UNITY_ANDROID
//        var info = ad.GetMediaExtraInfo();
//        Debug.Log($"OnRewardVideoAdLoad info:  expireTimestamp={info["expireTimestamp"]},materialMetaIsFromPreload={info["materialMetaIsFromPreload"]}");
//#endif
//        Debug.Log($"OnRewardVideoAdLoad on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
//        if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
//            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
//                PangleAdsManager.Instance.information = "OnRewardVideoAdLoad";

//        ad.SetRewardAdInteractionListener(this, callbackOnMainThread);
//        ad.SetAgainRewardAdInteractionListener(
//                new RewardAgainAdInteractionListener(PangleAdsManager.Instance), callbackOnMainThread);
//        ad.SetDownloadListener(
//            new AppDownloadListener(PangleAdsManager.Instance), callbackOnMainThread);

//        PangleAdsManager.Instance.rewardAd = ad;
//    }

//    public void OnExpressRewardVideoAdLoad(ExpressRewardVideoAd ad)
//    {
//    }

//    public void OnRewardVideoCached()
//    {
//        Debug.Log($"OnRewardVideoCached  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
//        if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
//            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
//                PangleAdsManager.Instance.information = "OnRewardVideoCached";
//#if UNITY_IOS
//            PangleAdsManager.Instance.rewardAd.setAuctionPrice(1.0);
//            PangleAdsManager.Instance.rewardAd.win(1.0);
//            PangleAdsManager.Instance.rewardAd.Loss(1.0,"102",bidder:"ylh");
//#endif
//    }

//    public void OnRewardVideoCached(RewardVideoAd ad)
//    {
//        Debug.Log($"OnRewardVideoCached RewardVideoAd ad on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
//    }


//}
