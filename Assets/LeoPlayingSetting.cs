using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoPlayerSetting : MonoBehaviour
{
    void Update()
    {
        //  按ESC退出全屏  
        if (Input.GetKey(KeyCode.Escape))
        {
            Screen.fullScreen = false;  //退出全屏           
        }
        //设置7680*1080的全屏  
        if (Input.GetKey(KeyCode.B))
        {
            Screen.SetResolution(1920, 1080, true);
        }
        if (Input.GetKey(KeyCode.C))
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
        //按A全屏  
        if (Input.GetKey(KeyCode.A))
        {
            //获取设置当前屏幕分辩率  
            UnityEngine.Resolution[] resolutions = Screen.resolutions;
            //设置当前分辨率  
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
            Screen.fullScreen = true;  //设置成全屏,  
        }
    }
}