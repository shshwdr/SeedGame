using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour, IUnityAdsShowListener
{
    public Button continueButton;
    public Button newGameButton;
    public Button supportButton;
    public Button creditButton;
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

        newGameButton.onClick.AddListener(delegate {

            FModSoundManager.Instance.restartGame();

        });
        supportButton.onClick.AddListener(delegate {

            PopupDialogue.createPopupDialogue(Dialogues.dialogues["supportDialog"], () =>
            {
                Debug.Log("pop up for hint");
                isActive = true;
                AdsManager.Instance.ShowAd(this);
            });

        });
        creditButton.onClick.AddListener(delegate {
            PopupDialogue.createPopupDialogue(Dialogues.dialogues["credits"]);

        });


    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isActive = false;

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Unity Ads  show click.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (isActive&& placementId.Equals(AdsManager.Instance._unitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.

            PopupDialogue.createPopupDialogue(Dialogues.dialogues["thanksDialog"]);

            // Load another ad:
            AdsManager.Instance.Load();
        }
        else
        {

        }
        isActive = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        isActive = false;
        Debug.LogError($"Unity Ads Show Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Unity Ads Start show.");
    }
}
