using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeScript : MonoBehaviour
{
    AudioSource source;
    KilledByItToSpawn killScript;
    public AudioClip[] clips;
    int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        killScript = GetComponent<KilledByItToSpawn>();
    }

    public void killScriptVisible(bool isVisible)
    {
        killScript.enabled = isVisible;
    }
    public void playSound()
    {
        source.PlayOneShot(clips[id]);
        id = 1 - id;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
