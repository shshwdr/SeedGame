using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUIView : MonoBehaviour
{
    public List<GameObject> gameobjects;
    public void show()
    {
        foreach(var ob in gameobjects)
        {
            ob.SetActive(false);
        }
    }
    public void hide()
    {

        foreach (var ob in gameobjects)
        {
            ob.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
