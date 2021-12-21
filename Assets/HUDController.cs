using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    public GameObject Book;

    public void ShowBook()
    {
        Book.SetActive(true);
    }
    public void HideBook()
    {
        Book.SetActive(false);
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
