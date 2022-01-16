using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBusVolume : MonoBehaviour
{
    public Slider slider;
    public string groupName;
    string finalString;
    public void changeVolume(float volume)
    {
        FMOD.Studio.Bus masterBus;

        masterBus = FMODUnity.RuntimeManager.GetBus(finalString);
        masterBus.setVolume(volume);
        PlayerPrefs.SetFloat(finalString, volume);
    }
    // Start is called before the first frame update
    void Start()
    {
        finalString = "bus:/" + groupName;
        slider.onValueChanged.AddListener(delegate { changeVolume(slider.value); });
        //read from playerPref
        if (PlayerPrefs.HasKey(finalString))
        {
            //changeVolume( PlayerPrefs.GetFloat(finalString));
            slider.value = PlayerPrefs.GetFloat(finalString);
        }
        else
        {

            //changeVolume(1);
            slider.value = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
