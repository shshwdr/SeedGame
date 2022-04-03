using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    public GameObject Book;

    public void ShowBook()
    {
        Book.SetActive(true);
        Book.GetComponentInChildren<Book>(true).UpdateSprites();
        AudioManager.Instance.playOpenBook();
    }
    public void HideBook()
    {
        Book.SetActive(false);
        AudioManager.Instance.playCloseBook();
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
