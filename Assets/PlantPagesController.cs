using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPagesController : MonoBehaviour
{
    PlantOnePage[] pages;
    public void init(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if(i == index)
            {
                pages[i].gameObject.SetActive(true);
                pages[i].init();
            }
            else
            {

                pages[i].gameObject.SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        pages = GetComponentsInChildren<PlantOnePage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
