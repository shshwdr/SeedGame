using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //AudioSource audio;
    //public AudioClip waterClip;

    //public AudioClip insect;
    //public AudioClip[] hoeSwring;
    //public AudioClip[] jump;
    //public AudioClip plantGrow;
    //public AudioClip die;

    public EventReference plantGrowEvent;

    public EventReference dieEvent;

    public EventReference menuButtonEvent;
    public EventReference turnalDown;
    public EventReference turnalUp;
    public EventReference turnalSpawn;
    public EventReference bug;
    public EventReference hoe;
    public EventReference fallOnLotusHigh;
    public EventReference fallOnLotusLow;



    public EventReference seedDrown;
    public EventReference seedRespawn;
    public EventReference seedJump;

    public EventReference endMessage;



    public EventReference openBook;

    public EventReference closeBook;

    public EventReference turnPage;


    public void playOpenBook()
    {

        RuntimeManager.PlayOneShot(openBook);
    }
    public void playCloseBook()
    {

        RuntimeManager.PlayOneShot(closeBook);
    }
    public void playTurnPage()
    {

        RuntimeManager.PlayOneShot(turnPage);
    }

    void initVolume(string groupName)
    {
        var finalString = "bus:/" + groupName;
        //read from playerPref
        float value = 1;
        if (PlayerPrefs.HasKey(finalString))
        {
            //changeVolume( PlayerPrefs.GetFloat(finalString));
            value = PlayerPrefs.GetFloat(finalString);
        }

        FMOD.Studio.Bus masterBus;

        masterBus = FMODUnity.RuntimeManager.GetBus(finalString);
        masterBus.setVolume(value);
        PlayerPrefs.SetFloat(finalString, value);
    }

    // Start is called before the first frame update
    void Start()
    {
        initVolume("Music");
        initVolume("SFX");


        //audio = GetComponent<AudioSource>();
        //clipToId = new Dictionary<AudioClip, int>();
    }

    public void playSeedRespawn()
    {
        RuntimeManager.PlayOneShot(seedRespawn);
    }
    public void playEndMessage()
    {
        RuntimeManager.PlayOneShot(endMessage);
    }

    public void playWater()
    {
        //RuntimeManager.PlayOneShot(playWater);
        //playAudio(waterClip);
        RuntimeManager.PlayOneShot(seedDrown);
    }
    public void playPlantGrow()
    {

        //playAudio(plantGrow);
        RuntimeManager.PlayOneShot(plantGrowEvent);
        //FMODUnity.RuntimeManager.CreateInstance(plantGrowEvent);
    }
    public void playDie()
    {
        RuntimeManager.PlayOneShot(dieEvent);
        //var ins = FMODUnity.RuntimeManager.CreateInstance(dieEvent);
        //ins.start();
        //playAudio(die);
    }
    public void playInsect()
    {
        RuntimeManager.PlayOneShot(bug);
        //playAudio(insect);
    }
    public void playJump()
    {
         RuntimeManager.PlayOneShot(seedJump);
        //playMultipleClips(jump);
    }
    public void playHoeSwing()
    {
        RuntimeManager.PlayOneShot(hoe);
        //playMultipleClips(hoeSwring);
    }

    public void playMenuButton()
    {
        RuntimeManager.PlayOneShot(menuButtonEvent);
    }

    public void playTunnelDown()
    {
        RuntimeManager.PlayOneShot(turnalDown);
    }

    public void playTunnelUp()
    {
        RuntimeManager.PlayOneShot(turnalUp);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
