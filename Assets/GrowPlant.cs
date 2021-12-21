using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        AudioManager.Instance.playPlantGrow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
