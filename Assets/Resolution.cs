using UnityEngine;
using System.Collections;


public class Resolution : MonoBehaviour
{       /// <summary>
        /// 开发屏幕的宽
        /// </summary>
    public static float DevelopWidth = 1334f;

    /// <summary>
    /// 开发屏幕的长
    /// </summary>
    public static float DevelopHeigh = 750f;

    /// <summary>
    /// 开发高宽比
    /// </summary>
    public static float DevelopRate = DevelopHeigh / DevelopWidth;

    /// <summary>
    /// 设备自身的高
    /// </summary>
    public static int curScreenHeight = Screen.height;

    /// <summary>
    /// 设备自身的高
    /// </summary>
    public static int curScreenWidth = Screen.width;

    /// <summary>
    /// 当前屏幕高宽比
    /// </summary>
    public static float ScreenRate = (float)Screen.height / (float)Screen.width;

    /// <summary>
    /// 世界摄像机rect高的比例
    /// </summary>
    public static float cameraRectHeightRate = DevelopHeigh / ((DevelopWidth / Screen.width) * Screen.height);

    /// <summary>
    /// 世界摄像机rect宽的比例
    /// </summary>
    public static float cameraRectWidthRate = DevelopWidth / ((DevelopHeigh / Screen.height) * Screen.width);
    public Camera mainCamera;
    void Start()
    {
        //Screen.SetResolution(1334, 750, true, 60);
        //mainCamera = Camera.main;
        //  float screenAspect = 1334 / 750;  //现在android手机的主流分辨。
        ////  mainCamera.aspect --->  摄像机的长宽比（宽度除以高度）
        //mainCamera.aspect = screenAspect;

    }

    private void Awake()
    {
        FitCamera(Camera.main);
    }

    public void FitCamera(Camera camera)
    {
        ///适配屏幕。实际屏幕比例<=开发比例的 上下黑  反之左右黑
        if (DevelopRate <= ScreenRate)
        {
            camera.rect = new Rect(0, (1 - cameraRectHeightRate) / 2, 1, cameraRectHeightRate);
        }
        else
        {
            camera.rect = new Rect((1 - cameraRectWidthRate) / 2, 0, cameraRectWidthRate, 1);
        }
    }
    void Update()
    {
        ////  按ESC退出全屏  
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Screen.fullScreen = false;  //退出全屏           
        //}
        ////设置7680*1080的全屏  
        //if (Input.GetKey(KeyCode.B))
        //{
        //    Screen.SetResolution(1920, 1080, true);
        //}
        //if (Input.GetKey(KeyCode.C))
        //{
        //    Screen.SetResolution(Screen.width, Screen.height, true);
        //}
        ////按A全屏  
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //获取设置当前屏幕分辩率  
        //    UnityEngine.Resolution[] resolutions = Screen.resolutions;
        //    //设置当前分辨率  
        //    Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        //    Screen.fullScreen = true;  //设置成全屏,  
        //}
    }
}