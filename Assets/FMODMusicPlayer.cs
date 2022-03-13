using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODMusicPlayer : MonoBehaviour
{
    float currentAmbienceIntensity;

    public EventReference[] EventReferences;
    List<EventInstance> musicInstances;
    // Start is called before the first frame update
    void Start()
    {
        startPlaying();
        DontDestroyOnLoad(gameObject);
    }

    public void startPlaying()
    {
        musicInstances = new List<EventInstance>();
        for(int i = 0;i< EventReferences.Length; i++)
        {
            var musicInstance = new EventInstance();
            musicInstances.Add(musicInstance);
            var eventDescription = RuntimeManager.GetEventDescription(EventReferences[i]);
            eventDescription.createInstance(out musicInstance);
            musicInstance.start();
        }
    }
    public void SetParam(string paramName, float value)
    {
        foreach(var musicInstance in musicInstances)
        {

            musicInstance.setParameterByName(paramName, value);
        }
    }


    //public override void Save(SerializedGame save)
    //{
    //    base.Save(save);
    //    save.musicParam = currentAmbienceIntensity;

    //}
    //public override void Load(SerializedGame save)
    //{
    //    base.Load(save);
    //    SetAmbienceParamter(save.musicParam);
    //}

    public void SetAmbienceParamter(float param)
    {
        if (param <= currentAmbienceIntensity)
        {
            return;
        }
        //if (currentAmbienceIntensity != param)
        {
            print("set ambience to " + param);
            currentAmbienceIntensity = param;
            foreach (var musicInstance in musicInstances)
            {
                musicInstance.setParameterByName("Intensity", param);
            }
            //ambience.setParameterByName()
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        gotoNextLevel();
    }

    public bool hasPreviousGame()
    {
        return SaveLoadManager.hasSavedData();
    }

    public void gotoNextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void restartGame()
    {
        if (hasPreviousGame())
        {

            //popup and ask if you really want to restart
            PopupDialogue.Instance.createPopupDialogue(Dialogues.dialogues["restart"], () =>
            { 
                Debug.Log("restart");
                SaveLoadManager.clearSavedData();
                gotoNextLevel();
            });
        }
        else
        {

            Debug.Log("new");
            SaveLoadManager.clearSavedData();
            gotoNextLevel();
        }
    }
}
