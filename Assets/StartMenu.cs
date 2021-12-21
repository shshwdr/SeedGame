using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;
    // Start is called before the first frame update
    void Start()
    {
        //Screen.orientation = ScreenOrientation.AutoRotation;
        //Screen.autorotateToLandscapeLeft = true;
        //Screen.autorotateToLandscapeRight = true;
        if (FModSoundManager.Instance. hasPreviousGame())
        {
            continueButton.gameObject.SetActive(true);

        }
        else
        {

            continueButton.gameObject.SetActive(false);
        }
        continueButton.onClick.AddListener(delegate { FModSoundManager.Instance.startGame(); });

        newGameButton.onClick.AddListener(delegate { FModSoundManager.Instance.restartGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
