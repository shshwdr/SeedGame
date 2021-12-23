using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class PlantOnePage : MonoBehaviour, IUnityAdsShowListener
{
    public string plantName;
    public TMP_Text plantDescriptionLabel;
    public Button plantHintButton;
    public TMP_Text plantHintLabel;
    public Image plantImage;

    public void init()
    {
        bool showHint = PlantManager.Instance.isPlantHinted(plantName);
        if (PlantManager.Instance.isPlantUnlocked(plantName))
        {

            plantImage.color = Color.white;
            plantHintLabel.gameObject.SetActive(true);
            plantDescriptionLabel.gameObject.SetActive(true);
            plantHintButton.gameObject.SetActive(false);
        }
        else
        {
            plantImage.color = Color.black;
            plantHintLabel.gameObject.SetActive(showHint);
            plantDescriptionLabel.gameObject.SetActive(false);
            plantHintButton.gameObject.SetActive(!showHint);
        }
    }

    public void clickHintButton()
    {

        AdsManager.Instance.ShowAd(this);

        //PlantManager.Instance.showPlantHint(plantName);
        //init();
    }
    // Start is called before the first frame update
    void Start()
    {
        plantHintButton.onClick.AddListener(delegate { clickHintButton(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {

        if (plantName == "")
        {
            plantName = name;
        }
    }



    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Unity Ads  show click.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(AdsManager.Instance._unitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            PlantManager.Instance.showPlantHint(plantName);
            init();
            // Load another ad:
            AdsManager.Instance.Load();
        }
        else
        {

        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Unity Ads Show Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Unity Ads Start show.");
    }
}
