using System;
using ByteDance.Union;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Collections;
using Pool;

public class PangleAdsManager : Singleton<PangleAdsManager>
{
    private RewardVideoAd rewardAd;
    private static int mainThreadId;
    private InputField screenOrientation;
    private bool showDownloadConfirmDialog = false;
    private AdNative adNative;

    public string plantName;
    public string information;

    float adTime;
    float adInvalidTime = 180;

    string oaid = "fake";
    bool sdkInited = false;

    private void Awake()
    {
        mainThreadId = Thread.CurrentThread.ManagedThreadId;
    }

    private static void callbackmethod(bool success, string message)
    {
        Debug.Log("`````````````````初始化``````" + success + "-----" + message);
    }

    IEnumerator fakeFetchOAID()
    {
        Debug.Log("fake fetch oaid start");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("fake fetch oaid");
        oaid = "cstest fake OAID";

        initSDK();
    }

    void fetchOAID()
    {

       // logdb.text = "";
        DeviceIDHelper.inst.getDeviceID((err, res) =>
        {
            if (err != null)
            {
                Debug.Log("oaid error "+err.Message);
                return;
                //logdb.text += err.Message + "\r\n";
            }
            bool isSucceed = false;
            foreach (var v in res)
            {
                isSucceed = true;
                Debug.Log("id: " + v);
                oaid = v;
                //logdb.text += "id: " + v + "\r\n";
            }
            if (isSucceed)
            {
                Debug.Log("succeed load oaid");
                initSDK();
            }
        }, true);
    }

    void initSDK()
    {
        Debug.Log("init sdk with oaid "+oaid);
        Pangle.InitializeSDK(callbackmethod, new CustomConfiguration
        {
            CanUseLocation = false,
            CanReadAppList = false,
            CanUsePhoneState = false,
            CanUseWifiState = false,
            CanUseWriteExternal = false,
            MacAddress = "fake mac address",
            //Latitude = 35.23,
            //Longitude = 139.12,
            DevImei = "fake",
            DevOaid = oaid
        });
        sdkInited = true;
        LoadRewardAd();
    }

    public static bool callbackOnMainThread = true;
    public void OnCallbackThreadChange(Toggle value)
    {
        callbackOnMainThread = value.isOn;
        Debug.Log($"callbackOnMainThread = {showDownloadConfirmDialog}");
    }

    void Start()
    {
#if UNITY_IOS
        PangleConfiguration configuration = PangleConfiguration.CreateInstance();
        configuration.appID = "5000546";
#endif
       // StartCoroutine(fakeFetchOAID());
        fetchOAID();
        initSDK();
        //LoadRewardAd();
    }

    private AdNative AdNative
    {
        get
        {
            if (this.adNative == null)
            {
                this.adNative = SDK.CreateAdNative();
            }
#if UNITY_ANDROID
            SDK.RequestPermissionIfNecessary();
#endif
            return this.adNative;
        }
    }

    // 激励和全屏，开屏的横竖屏配置
    public int getScreenOrientationConfig()
    {
        //this.screenOrientation = GameObject.Find("Canvas/Container/screenOrientation").GetComponent<InputField>();
        //int screenOrientation;
        //int.TryParse(this.screenOrientation.text, out screenOrientation);

        return 1;
    }

    /// <summary>
    /// Load the reward Ad.
    /// </summary>
    public void LoadRewardAd()
    {
        //if (!sdkInited)
        //{
        //    initSDK();
        //}
        if (this.rewardAd != null)
        {
            if(Time.time - adTime > adInvalidTime)
            {

                this.rewardAd.Dispose();
                Debug.Log("cstest dispose add ");
                this.rewardAd = null;
            }
            else
            {
                Debug.Log("cstest dont dispose ");
                return;
            }

        }

        string iosSlotID = "900546826";
        string AndroidSlotID = "948273328";
        // 横屏
        if (this.getScreenOrientationConfig() == 1)
        {
            iosSlotID = "900546319";
            AndroidSlotID = "948273328";
        }

        var adSlot = new AdSlot.Builder()
#if UNITY_IOS
            .SetCodeId(iosSlotID)
#else
            .SetCodeId(AndroidSlotID)
#endif
            .SetSupportDeepLink(true)
            .SetImageAcceptedSize(1080, 1920)
            .SetUserID("user123") // 用户id,必传参数
            .SetMediaExtra("media_extra") // 附加参数，可选
            .SetOrientation(AdOrientation.Horizontal) // 必填参数，期望视频的播放方向
#if UNITY_ANDROID
            .SetDownloadType(showDownloadConfirmDialog ? DownloadType.DownloadTypePopup : DownloadType.DownloadTypeNoPopup)
#endif
            .Build();
        Debug.Log("cstest LoadRewardAd");
        this.AdNative.LoadRewardVideoAd(adSlot, new RewardVideoAdListener(this), callbackOnMainThread);
        adTime = Time.time;
    }

    /// <summary>
    /// Show the reward Ad.
    /// </summary>
    public void ShowRewardAd()
    {
        if (this.rewardAd == null)
        {
            LoadRewardAd();
            StartCoroutine(showad());
            return;
        }
        else
        {
            this.rewardAd.ShowRewardVideoAd();
        }

    }

    IEnumerator showad()
    {
        int waitTime = 7;
        while(this.rewardAd == null && waitTime>=0)
        {
            waitTime--;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        if (this.rewardAd != null)
        {

            this.rewardAd.ShowRewardVideoAd();
        }
        else
        {
            Debug.LogError("请先加载广告");
            this.information = "请先加载广告";
            yield break;
        }
    }


    private sealed class RewardVideoAdListener : IRewardVideoAdListener
    {
        private PangleAdsManager pangleAdsManager;

        public RewardVideoAdListener(PangleAdsManager pangleAdsManager)
        {
            this.pangleAdsManager = pangleAdsManager;
        }

        public void OnError(int code, string message)
        {
            Debug.LogError($"OnRewardError: {message}  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "OnRewardError: " + message;
        }

        public void OnRewardVideoAdLoad(RewardVideoAd ad)
        {
#if UNITY_ANDROID
            var info = ad.GetMediaExtraInfo();
            Debug.Log($"OnRewardVideoAdLoad info:  expireTimestamp={info["expireTimestamp"]},materialMetaIsFromPreload={info["materialMetaIsFromPreload"]}");
#endif
            Debug.Log($"OnRewardVideoAdLoad on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                    this.pangleAdsManager.information = "OnRewardVideoAdLoad";

            ad.SetRewardAdInteractionListener(
                new RewardAdInteractionListener(this.pangleAdsManager), callbackOnMainThread);
            ad.SetAgainRewardAdInteractionListener(
                    new RewardAgainAdInteractionListener(this.pangleAdsManager), callbackOnMainThread);
            ad.SetDownloadListener(
                new AppDownloadListener(this.pangleAdsManager), callbackOnMainThread);
            Debug.Log("cstest add reward ad " + ad);
            this.pangleAdsManager.rewardAd = ad;
        }

        public void OnExpressRewardVideoAdLoad(ExpressRewardVideoAd ad)
        {
        }

        public void OnRewardVideoCached()
        {
            Debug.Log($"OnRewardVideoCached  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                    this.pangleAdsManager.information = "OnRewardVideoCached";
#if UNITY_IOS
            this.pangleAdsManager.rewardAd.setAuctionPrice(1.0);
            this.pangleAdsManager.rewardAd.win(1.0);
            this.pangleAdsManager.rewardAd.Loss(1.0,"102",bidder:"ylh");
#endif
        }

        public void OnRewardVideoCached(RewardVideoAd ad)
        {
            Debug.Log($"OnRewardVideoCached RewardVideoAd ad on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
        }
    }

    private sealed class RewardAdInteractionListener : IRewardAdInteractionListener
    {
        private PangleAdsManager pangleAdsManager;

        public RewardAdInteractionListener(PangleAdsManager pangleAdsManager)
        {
            this.pangleAdsManager = pangleAdsManager;
        }

        public void OnAdShow()
        {
            Debug.Log($"rewardVideoAd show  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "rewardVideoAd show";
        }

        public void OnAdVideoBarClick()
        {
            Debug.Log($"rewardVideoAd bar click  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "rewardVideoAd bar click";
        }

        public void OnAdClose()
        {
            Debug.Log($"rewardVideoAd close  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "rewardVideoAd close";
            if (this.pangleAdsManager.rewardAd != null)
            {
                this.pangleAdsManager.rewardAd.Dispose();
                this.pangleAdsManager.rewardAd = null;
                Debug.Log("cstest dispose add ");
            }
#if UNITY_IOS
            if (this.pangleAdsManager.expressRewardAd != null) {
                this.pangleAdsManager.expressRewardAd.Dispose();
                this.pangleAdsManager.expressRewardAd = null;
            }
#endif

        }

        public void OnVideoSkip()
        {
            Debug.Log($"rewardVideoAd skip  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "rewardVideoAd skip";
        }


        public void OnVideoComplete()
        {
            Debug.Log($"rewardVideoAd complete  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "rewardVideoAd complete";
        }

        public void OnVideoError()
        {
            Debug.LogError($"rewardVideoAd error  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "rewardVideoAd error";
        }

        public void OnRewardVerify(
            bool rewardVerify, int rewardAmount, string rewardName)
        {
            Debug.Log("rewardVideoAd verify:" + rewardVerify + " amount:" + rewardAmount +
                      " name:" + rewardName + $" on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information =
                "verify:" + rewardVerify + " amount:" + rewardAmount +
                " name:" + rewardName;
            if (rewardVerify)
            {
                PlantManager.Instance.showPlantHint(pangleAdsManager.plantName);

                EventPool.Trigger("updatePlant");
            }
            this.pangleAdsManager.LoadRewardAd();
        }
    }

    private sealed class RewardAgainAdInteractionListener : IRewardAdInteractionListener
    {
        private PangleAdsManager pangleAdsManager;

        public RewardAgainAdInteractionListener(PangleAdsManager pangleAdsManager)
        {
            this.pangleAdsManager = pangleAdsManager;
        }

        public void OnAdShow()
        {
            Debug.Log($"again rewardVideoAd show  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "again rewardVideoAd show";
        }

        public void OnAdVideoBarClick()
        {
            Debug.Log($"again rewardVideoAd bar click  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "again rewardVideoAd bar click";
        }

        public void OnAdClose()
        {

        }

        public void OnVideoSkip()
        {
            Debug.Log($"again rewardVideoAd skip  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "again rewardVideoAd skip";
        }


        public void OnVideoComplete()
        {
            Debug.Log($"again rewardVideoAd complete  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "again rewardVideoAd complete";
        }

        public void OnVideoError()
        {
            Debug.LogError($"again rewardVideoAd error  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information = "again rewardVideoAd error";
        }

        public void OnRewardVerify(
            bool rewardVerify, int rewardAmount, string rewardName)
        {
            Debug.Log("again rewardVideoAd verify:" + rewardVerify + " amount:" + rewardAmount +
                      " name:" + rewardName + $" on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.pangleAdsManager.information =
                "again verify:" + rewardVerify + " amount:" + rewardAmount +
                " name:" + rewardName;
        }
    }

    private sealed class AppDownloadListener : IAppDownloadListener
    {
        private PangleAdsManager example;

        public AppDownloadListener(PangleAdsManager example)
        {
            this.example = example;
        }

        public void OnIdle()
        {
        }

        public void OnDownloadActive(
            long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.Log($"下载中，点击下载区域暂停  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.example.information = "下载中，点击下载区域暂停";
        }

        public void OnDownloadPaused(
            long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.Log($"下载暂停，点击下载区域继续  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId} ");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.example.information = "下载暂停，点击下载区域继续";
        }

        public void OnDownloadFailed(
            long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.LogError($"下载失败，点击下载区域重新下载  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.example.information = "下载失败，点击下载区域重新下载";
        }

        public void OnDownloadFinished(
            long totalBytes, string fileName, string appName)
        {
            Debug.Log($"下载完成  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.example.information = "下载完成";
        }

        public void OnInstalled(string fileName, string appName)
        {
            Debug.Log($"安装完成，点击下载区域打开  on main thread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                this.example.information = "安装完成，点击下载区域打开";
        }
    }
}
