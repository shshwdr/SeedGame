using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audio;
    public AudioClip waterClip;

    public AudioClip insect;
    public AudioClip[] hoeSwring;
    public AudioClip[] jump;
    public AudioClip plantGrow;
    public AudioClip die;

    public EventReference dieEvent;

    Dictionary<AudioClip, int> clipToId;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        clipToId = new Dictionary<AudioClip, int>();
    }

    void playMultipleClips(AudioClip[] clips)
    {
        var firstClip = clips[0];
        if (!clipToId.ContainsKey(firstClip))
        {
            clipToId[firstClip] = 0;
        }
        audio.PlayOneShot(clips[clipToId[firstClip]]);
        clipToId[firstClip]++;
        if (clipToId[firstClip] >= clips.Length)
        {
            clipToId[firstClip] = 0;
        }
    }



    void playAudio(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }


    public void playWater()
    {
        playAudio(waterClip);
    }
    public void playPlantGrow()
    {
        //playAudio(plantGrow);
        //FMODUnity.RuntimeManager.CreateInstance(dieEvent);
    }
    public void playDie()
    {
        var ins = FMODUnity.RuntimeManager.CreateInstance(dieEvent);
        ins.start();
        //playAudio(die);
    }
    public void playInsect()
    {
        playAudio(insect);
    }
    public void playJump()
    {
        //playMultipleClips(jump);
    }
    public void playHoeSwing()
    {
        playMultipleClips(hoeSwring);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
