using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicParamterAtStart : MonoBehaviour
{
    public float changeTo;
    // Start is called before the first frame update
    void Start()
    {
        FModSoundManager.Instance.SetAmbienceParamter(changeTo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
