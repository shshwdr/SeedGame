using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PopupDialogue:Singleton<PopupDialogue>
{

    public TMP_Text text;
    public Button yesButton;

    public Button noButton;

    public float duration = 0.3f;

    GameObject prefab;
    Transform canvas;
    private void Awake()
    {
        prefab = Resources.Load<GameObject>("UI/PopupDialog");
    }

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("MainCanvas").transform;
    }

    public void createPopupDialogue(string t, Action y = null, string yesString = "YES")
    {
        //var prefab = Resources.Load<GameObject>("UI/PopupDialog");
        if (!prefab)
        {
            return;
        }
        if (!canvas)
        {
            Debug.Log("find canvas");
            canvas = GameObject.Find("MainCanvas").transform;
        }
        var go = Instantiate(prefab, canvas);
        go.GetComponent<PopupDialogue>().Init(t, y, yesString);
    }


    public void Init(string t, Action y, string yesString)
    {
        // group.alpha = 1;
        //// group.interactable = true;
        // group.blocksRaycasts = true;
        text.text = t;

        clearButton();

        if (y == null)
        {
            noButton.gameObject.SetActive(false);
            yesButton.onClick.AddListener(delegate
            {
                Hide();
            });
        }
        else
        {


            yesButton.onClick.AddListener(delegate
            {
                y(); Hide();
            });
            noButton.onClick.AddListener(delegate
            {
                Hide();
            });
        }

        yesButton.GetComponentInChildren<TMP_Text>().text = yesString;
        Time.timeScale = 0;
    }

    void clearButton()
    {

        yesButton.onClick.RemoveAllListeners();

        noButton.onClick.RemoveAllListeners();
    }



    public void Hide()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        //group.alpha = 0;
        //group.interactable = false;
        //group.blocksRaycasts = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
