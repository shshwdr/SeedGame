using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    EventInstance snapshot;
    public GameObject hideUILayer;
    public void OnBackToMenuButton()
    {
        SaveLoadManager.Instance.saveGame();
        SceneManager.LoadScene(0);
    }

    public void hideUI()
    {

        closeSetting();
        hideUILayer.SetActive(true);
        hideUILayer.GetComponent<HideUIView>().show();
    }

    public void selectLanguage(int l)
    {
        Translator.Instance.SetDisplayLanguage(l);
    }
    public void respawn()
    {
        closeSetting();
        GameObject.FindObjectOfType<PlayerMovement>().suicide();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void openSetting()
    {
        //snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Pause");
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(snapshot, Transform, Rigidbody);
        //FMODUnity.RuntimeManager.at
        //snapshot.start();
        Time.timeScale = 0;
    }

    public void closeSetting()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        //snapshot.stop(STOP_MODE.ALLOWFADEOUT);
    }
    public void playMenuSound()
    {
        AudioManager.Instance.playMenuButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
